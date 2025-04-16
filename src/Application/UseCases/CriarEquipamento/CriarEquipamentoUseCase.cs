// Importações necessárias
using Application.DTOs;
using Application.DTOs.Requests; // DTO de entrada (request)
using Application.Services.Token;
using Application.UseCases.CriarEquipamento.Validadores;
using AutoMapper; // Biblioteca AutoMapper para mapear objetos
using Domain.Entities; // Entidade de domínio Equipamento
using Domain.Exceptions; // Exceções personalizadas da aplicação
using Domain.Repositories.EquipamentoRepository; // Interface do repositório

namespace Application.UseCases.CriarEquipamento
{
    // A classe CriarEquipamentoUseCase implementa a interface ICriarEquipamentoUseCase
    // Isso define o contrato que a aplicação usa para criar equipamentos
    public class CriarEquipamentoUseCase : ICriarEquipamentoUseCase
    {
        // Campo privado que guarda a instância do repositório de equipamentos (injeção de dependência)
        private readonly IEquipamentoRepository _repository;

        // Instância do AutoMapper que será usada para transformar DTOs em entidades
        public IMapper _mapper;

        // Instância do Servico de token  que será usada para validar  o token enviado pela requisiçao
        private readonly IServicoDeToken _servicoDeToken;

        // Construtor da classe, recebe via injeção de dependência:
        // - o repositório que lida com persistência
        // - o mapper que converte DTOs em entidades
        public CriarEquipamentoUseCase(IEquipamentoRepository repository, IMapper mapper, IServicoDeToken servicoDeToken)
        {
            _repository = repository;
            _mapper = mapper;
            _servicoDeToken = servicoDeToken;
        }

        // Método principal da classe: Executa o caso de uso de criação de um equipamento
        // Parâmetro:
        // - request: objeto contendo os dados enviados pela requisição para criação do equipamento
        public async Task<RespostaDeSucessoDaApi<object>> Execute(RegistraEquipamentoRequest request)
        {
            // 1. Valida os dados recebidos na requisição para garantir que estão corretos antes de prosseguir.   
            Validador(request);
 
            // 2. Mapeia os dados do request para a entidade de domínio Equipamento.
            // A instância do equipamento será criada a partir dos dados da requisição para que possamos manipulá-la internamente.
            var equipamento = _mapper.Map<Equipamento>(request);

            // 3. Chama o repositório para persistir os dados do novo equipamento no banco.
            await _repository.AdicionarEquipamentoAsync(equipamento);

            // 4. Retorna uma resposta indicando sucesso com os dados principais.
            return new RespostaDeSucessoDaApi<object>
            {
                Succes = true,
                Message = "Equipamento cadastrado com sucesso",
                Data = new { Nome = request.Nome }
            };
        }

        // Método auxiliar que executa a validação da requisição recebida
        // Lança uma exceção com os erros encontrados, se houver.
        private void Validador(RegistraEquipamentoRequest request)
        {
            // 1. Cria uma instância do validador de equipamento
            var validar = new ValidadorDeCriacaoDeEquipamento();

            // 2. Executa a validação dos dados da requisição.
            // A validação verifica se os dados fornecidos estão no formato correto e atendem às regras de negócio definidas.
            var resultado = validar.Validate(request);

            // 3. Se houver erros na validação, lança uma exceção contendo as mensagens de erro.
            if (!resultado.IsValid)
            {
                // Coleta todas as mensagens de erro geradas pela validação e as transforma em uma lista.
                var mensagensDeErro = resultado.Errors.Select(erro => erro.ErrorMessage).ToList();

                // Lança uma exceção personalizada contendo os erros encontrados.
                // A exceção `ErroDeValidacaoException` será usada para informar ao usuário os erros de validação que ocorreram.
                throw new ErroDeValidacaoException(mensagensDeErro);
            }
        }

        public void ValidarToken(string token)
        {
            _servicoDeToken.ValidarToken(token);
        }
    }
}
