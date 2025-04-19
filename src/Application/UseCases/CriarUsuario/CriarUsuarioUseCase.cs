using System.Security.Cryptography.X509Certificates;
using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.Services.Criptografia;
using Application.Services.Token;
using Application.UseCases.CriarUsuario.Validadores;
using AutoMapper;
using Domain;
using Domain.Exceptions;
using Domain.Repositories.UsuarioRepository;

namespace Application.UseCases.CriarUsuario
{
    /// <summary>
    /// A classe CriarUsuarioUseCase implementa a interface ICriarUsuarioUseCase, 
    /// que define o contrato para a criação de um usuário.
    /// </summary>
    public class CriarUsuarioUseCase : ICriarUsuarioUseCase
    {
        /// <summary>
        /// Instância do repositório de usuários (injeção de dependência).
        /// Responsável por interagir com o banco de dados para persistência do usuário.
        /// </summary>
        private readonly IUsuarioRepository _repository;

        /// <summary>
        /// Instância do AutoMapper usada para transformar DTOs em entidades.
        /// </summary>
        public IMapper _mapper;

        /// <summary>
        /// Serviço de criptografia para proteger senhas.
        /// </summary>
        private readonly ICriptografiaDeSenha _criptografiaDeSenha;

        /// <summary>
        /// Serviço de manipulação de tokens.
        /// </summary>
        private readonly IServicoDeToken _servicoDetoken;

        /// <summary>
        /// Construtor da classe, recebe via injeção de dependência:
        /// - o repositório que lida com persistência de dados
        /// - o AutoMapper que converte DTOs em entidades
        /// - o serviço de criptografia para senhas
        /// - o serviço de tokens
        /// </summary>
        public CriarUsuarioUseCase(IUsuarioRepository repository, IMapper mapper, ICriptografiaDeSenha criptografiaDeSenha, IServicoDeToken servicoDetoken)
        {
            _repository = repository;
            _mapper = mapper;
            _criptografiaDeSenha = criptografiaDeSenha;
            _servicoDetoken = servicoDetoken;
        }

        /// <summary>
        /// Método principal da classe: Executa o caso de uso de criação de um usuário.
        /// - Valida os dados da requisição
        /// - Mapeia os dados para a entidade de domínio `Usuario`
        /// - Criptografa a senha
        /// - Persiste o usuário no banco de dados
        /// </summary>
        /// <param name="request">Objeto contendo os dados enviados pela requisição para a criação do usuário</param>
        /// <returns>Resposta de sucesso com os dados principais do usuário criado</returns>
        public async Task<RespostaDeSucessoDaApi<Object>> Execute(RegistrarUsuarioRequest request)
        {
            // 1. Valida os dados recebidos na requisição para garantir que estão corretos antes de prosseguir.
            await Validador(request);

            // 2. Mapeia os dados da requisição para a entidade de domínio Usuario.
            var usuario = _mapper.Map<Domain.Entities.Usuario>(request);

            // 3. Criptografa a senha do usuário antes de salvar no banco de dados.
            usuario.Senha = _criptografiaDeSenha.CriptografarSenha(request.Senha);

            // 4. Chama o repositório para adicionar o usuário no banco de dados.
            await _repository.AdicionarAsync(usuario);

            // 5. Retorna uma resposta indicando sucesso com os dados principais.
            return new RespostaDeSucessoDaApi<object>
            {
                Succes = true,
                Message = "Usuário cadastrado com sucesso",
                Data = new { Nome = request.Nome }
            };
        }

        /// <summary>
        /// Método auxiliar que executa a validação da requisição recebida.
        /// - Verifica se o e-mail já está cadastrado no sistema
        /// - Lança exceções com os erros encontrados, se houver.
        /// </summary>
        /// <param name="request">Objeto com os dados da requisição</param>
        /// <exception cref="ErroDeValidacaoException">Exceção lançada se houver falhas de validação</exception>
        public async Task Validador(RegistrarUsuarioRequest request)
        {
            // 1. Cria uma instância do validador de criação de usuário.
            var validar = new ValidadorDeCriacaoDeUsuario();

            // 2. Executa a validação dos dados da requisição.
            var resultado = validar.Validate(request);

            // 3. Verifica se existe algum email já cadastrado no sistema.
            var emailExiste = await _repository.ExisteEmailCadastradoAsync(request.Email);

            // 4. Se o email já existir, adiciona uma mensagem de erro na validação.
            if (emailExiste)
                resultado.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, MensagensDeExceptionUsuario.EMAIL_JA_USADO));

            // 5. Se houver erros na validação, lança uma exceção personalizada com as mensagens de erro.
            if (!resultado.IsValid)
            {
                var mensagensDeErro = resultado.Errors.Select(erro => erro.ErrorMessage).ToList();
                throw new ErroDeValidacaoException(mensagensDeErro);
            }
        }
    }
}
