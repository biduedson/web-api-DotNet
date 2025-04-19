// ===========================
//         IMPORTAÇÕES
// ===========================

using Application.DTOs;
using Application.DTOs.Requests; // DTO de entrada (request)
using Application.DTOs.Responses;
using Application.Services.Token; // Serviço para validação de token
using Application.UseCases.CriarEquipamento.Validadores; // Classe de validação da request
using AutoMapper; // Biblioteca AutoMapper para mapear objetos
using Domain.Entities; // Entidade de domínio Equipamento
using Domain.Exceptions; // Exceções personalizadas da aplicação
using Domain.Repositories.EquipamentoRepository; // Interface do repositório

namespace Application.UseCases.CriarEquipamento
{
    /// <summary>
    /// Caso de uso responsável pela criação de um novo equipamento.
    /// - Implementa a interface ICriarEquipamentoUseCase
    /// - Utiliza injeção de dependência para o repositório, serviço de token e AutoMapper
    /// </summary>
    public class CriarEquipamentoUseCase : ICriarEquipamentoUseCase
    {
        /// <summary>
        /// Instância do repositório de equipamentos usada para persistência.
        /// </summary>
        private readonly IEquipamentoRepository _repository;

        /// <summary>
        /// Instância do AutoMapper usada para converter DTOs em entidades.
        /// </summary>
        public IMapper _mapper;

        /// <summary>
        /// Serviço utilizado para validar o token enviado na requisição.
        /// </summary>
        private readonly IServicoDeToken _servicoDeToken;

        /// <summary>
        /// Construtor responsável por injetar as dependências necessárias.
        /// - Repositório de equipamentos
        /// - Instância do AutoMapper
        /// - Serviço de validação de token
        /// </summary>
        /// <param name="repository">Repositório de equipamentos</param>
        /// <param name="mapper">Instância do AutoMapper</param>
        /// <param name="servicoDeToken">Serviço para validação de token</param>
        public CriarEquipamentoUseCase(IEquipamentoRepository repository, IMapper mapper, IServicoDeToken servicoDeToken)
        {
            _repository = repository;
            _mapper = mapper;
            _servicoDeToken = servicoDeToken;
        }

        /// <summary>
        /// Método principal da classe: Executa o caso de uso de criação de um equipamento.
        /// - Valida os dados da requisição
        /// - Mapeia os dados para a entidade de domínio `Equipamento`
        /// - Persiste o equipamento no banco de dados
        /// - Retorna uma resposta de sucesso com os dados do equipamento criado
        /// </summary>
        /// <param name="request">Objeto contendo os dados enviados na requisição</param>
        /// <returns>Resposta de sucesso com os dados do equipamento criado</returns>
        public async Task<RespostaDeSucessoDaApi<object>> Execute(RegistraEquipamentoRequest request)
        {
            // 1. Valida os dados recebidos na requisição para garantir que estão corretos antes de prosseguir.   
            Validador(request);
 
            // 2. Mapeia os dados do request para a entidade de domínio Equipamento.
            var equipamento = _mapper.Map<Equipamento>(request);

            // 3. Chama o repositório para persistir os dados no banco de dados.
            await _repository.AdicionarEquipamentoAsync(equipamento);

            // 4. Retorna uma resposta de sucesso com as informações do equipamento.
            return new RespostaDeSucessoDaApi<object>
            {
                Succes = true,
                Message = "Equipamento cadastrado com sucesso",
                Data = new { Nome = request.Nome }
            };
        }

        /// <summary>
        /// Método auxiliar responsável por validar os dados da requisição.
        /// - Executa a validação da requisição utilizando a classe de validação.
        /// - Lança exceção personalizada se a validação falhar.
        /// </summary>
        /// <param name="request">Objeto com os dados da requisição</param>
        /// <exception cref="ErroDeValidacaoException">Exceção lançada se a validação falhar</exception>
        private void Validador(RegistraEquipamentoRequest request)
        {
            // 1. Instancia o validador da request
            var validar = new ValidadorDeCriacaoDeEquipamento();

            // 2. Executa a validação
            var resultado = validar.Validate(request);

            // 3. Se houver erros, lança uma exceção com todas as mensagens
            if (!resultado.IsValid)
            {
                var mensagensDeErro = resultado.Errors.Select(erro => erro.ErrorMessage).ToList();
                throw new ErroDeValidacaoException(mensagensDeErro);
            }
        }

        /// <summary>
        /// Valida o token recebido na requisição utilizando o serviço apropriado.
        /// - Garante que o token JWT seja válido antes de permitir a execução da operação.
        /// </summary>
        /// <param name="token">Token JWT enviado pelo cliente</param>
        public void ValidarToken(string token)
        {
            _servicoDeToken.ValidarToken(token);
        }
    }
}
