

using System.Security.Cryptography.X509Certificates;
using Application.criptografia;
using Application.DTOs.Requests;
using Application.Services.Token;
using Application.UseCases.CriarUsuario.Validadores;
using AutoMapper;
using Domain;
using Domain.Exceptions;
using Domain.Repositories.UsuarioRepository;

namespace Application.UseCases.CriarUsuario
{
    // A classe CriarUsuarioUseCase implementa a interface ICriarUsuarioUseCase
    // Isso define o contrato que a aplicação usa para criar usuarios
    public class CriarUsuarioUseCase : ICriarUsuarioUseCase
    {
        // Campo privado que guarda a instância do repositório de usuarios (injeção de dependência
        private readonly IUsuarioRepository _repository;

         // Instância do AutoMapper que será usada para transformar DTOs em entidades
        public IMapper _mapper;

          // Serviço de criptografia para proteger senhas.
        private readonly CriptografiaDeSenha _criptografiaDeSenha;

         // Construtor da classe, recebe via injeção de dependência:
         // - o repositório que lida com persistência
         // - o mapper que converte DTOs em entidades
        
        // Serviço de manipulacao de tokens.
        private readonly IServicoDeToken _servicoDetoken;
        
        public CriarUsuarioUseCase(IUsuarioRepository repository, IMapper mapper, CriptografiaDeSenha criptografiaDeSenha, IServicoDeToken servicoDetoken)
        {
            _repository = repository;
            _mapper = mapper;
            _criptografiaDeSenha = criptografiaDeSenha;
            _servicoDetoken = servicoDetoken;

        }

         // Método principal da classe: Executa o caso de uso de criação de um usuario
         // Parâmetro:
         // - request: objeto contendo os dados enviados pela requisição para criação do usuario
        public async Task<RespostaDeSucessoDaApi<Object>> Execute(RegistrarUsuarioRequest request)
         {
          // 1. Valida os dados recebidos na requisição para garantir que estão corretos antes de prosseguir.   
           await  Validador(request);

           // 2. Mapeia os dados do request para a entidade de domínio User.
           // A instância do usuário será criada a partir dos dados da requisição para que possamos manipulá-la internamente.
           var usuario = _mapper.Map<Domain.Entities.Usuario>(request);

          // 3. Criptografa a senha do usuário antes de salvar no banco de dados.
          // A senha deve ser criptografada para garantir que dados sensíveis não sejam armazenados em formato simples.
           usuario.Senha = _criptografiaDeSenha.Criptografar(request.Senha);


           // 4. Aqui chamamos o repositório responsável por adicionar o usuário no banco de dados.
           await _repository.AdicionarAsync(usuario);

          // 5. Retorna uma resposta indicando sucesso com os dados principais.
            return new RespostaDeSucessoDaApi<object>
            {
                Succes = true,
                Message = "Usuario cadastrado com sucesso",
                Data = new { Nome = request.Nome }
            };
         }

          // Método auxiliar que executa a validação da requisição recebida
          // Lança uma exceção com os erros encontrados, se houver.
          public async Task Validador(RegistrarUsuarioRequest request)
          {
            // 1. Cria uma instância do validador de equipamento
            var validar = new ValidadorDeCriacaoDeUsuario();

            // 2. Executa a validação dos dados da requisição.
            // A validação verifica se os dados fornecidos estão no formato correto e atendem às regras de negócio definidas.
            var resultado = validar.Validate(request);

            // 3. Verifica se existe algum email ja cadastrado no sistema.
            var emailExiste = await _repository.ExisteEmailCadastradoAsync(request.Email);

            // 4. Se houver erros na validação, lança uma exceção contendo as mensagens de erro.
            if(emailExiste)
                   resultado.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, MensagensDeExceptionUsuario.EMAIL_JA_USADO));
                
            // 5. Se houver erros na validação, lança uma exceção contendo as mensagens de erro.
            if(!resultado.IsValid)
            {
               // Coleta todas as mensagens de erro geradas pela validação e as transforma em uma lista.
               var mensagensDeErro = resultado.Errors.Select(erro => erro.ErrorMessage).ToList();

               // Lança uma exceção personalizada contendo os erros encontrados.
               // A exceção `ErroDeValidacaoException` será usada para informar ao usuário os erros de validação que ocorreram.
               throw new ErroDeValidacaoException(mensagensDeErro);
            }
          }
       
    }
}