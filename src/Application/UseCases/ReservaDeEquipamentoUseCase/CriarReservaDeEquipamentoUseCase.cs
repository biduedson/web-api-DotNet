using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.Services.Token;
using Application.UseCases.ReservaDeEquipamentoUseCase.Validadores;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories.ReservaDeEquipamentosRepository;

namespace Application.UseCases.ReservaDeEquipamentoUseCase
{
    /// <summary>
    /// Caso de uso responsável pela criação de uma reserva de equipamento.
    /// Essa classe implementa a interface ICriarReservaDeEquipamentoUseCase, que define o contrato da operação.
    /// Utiliza injeção de dependência para o repositório, serviço de token e AutoMapper.
    /// </summary>
    public class CriarReservaDeEquipamentoUseCase : ICriarReservaDeEquipamentoUseCase
    {
        /// <summary>
        /// Instância do repositório de reserva de equipamento usada para persistência.
        /// </summary>
        private readonly IReservaDeEquipamentosRepository _reposotory;

        /// <summary>
        /// Instância do AutoMapper usada para converter DTOs em entidades.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Serviço utilizado para validar o token enviado na requisição.
        /// </summary>
        private readonly IServicoDeToken _servicoDeToken;

        /// <summary>
        /// Construtor responsável por injetar as dependências necessárias.
        /// </summary>
        /// <param name="repository">Repositório de reserva de equipamentos</param>
        /// <param name="mapper">Instância do AutoMapper</param>
        /// <param name="servicoDeToken">Serviço para validação de token</param>
        public CriarReservaDeEquipamentoUseCase(IReservaDeEquipamentosRepository reposotory, IMapper mapper, IServicoDeToken servicoDeToken)
        {
            _reposotory = reposotory;
            _mapper = mapper;
            _servicoDeToken = servicoDeToken;
        }

        /// <summary>
        /// Executa o processo de criação de reserva de equipamento.
        /// O método realiza a validação dos dados recebidos, mapeia os dados para a entidade correspondente
        /// e persiste as informações no banco de dados.
        /// </summary>
        /// <param name="request">Objeto contendo os dados enviados na requisição</param>
        /// <returns>Resposta de sucesso com os dados da reserva do equipamento criada</returns>
        public async Task<RespostaDeSucessoDaApi<ReservaDeEquipamento>> Execute(ReservaDeEquipamentoRequest request)
        {
            // 1. Valida os dados recebidos na requisição para garantir que estão corretos antes de prosseguir.
            Validador(request);

            // 2. Mapeia os dados do request para a entidade de domínio ReservaDeEquipamento.
            var reservaDeEquipamento = _mapper.Map<ReservaDeEquipamento>(request);

            // 3. Chama o repositório para persistir os dados no banco de dados.
            var reservaCriada = await _reposotory.AdicionarReserva(reservaDeEquipamento);

            // 4. Retorna uma resposta de sucesso com as informações da reserva do equipamento.
            return new RespostaDeSucessoDaApi<ReservaDeEquipamento>
            {
                Succes = true,
                Message = "Equipamento cadastrado com sucesso",
                Data = reservaCriada
            };
        }

        /// <summary>
        /// Método auxiliar responsável por validar os dados da requisição.
        /// Lança exceção personalizada se houver falhas de validação.
        /// </summary>
        /// <param name="request">Objeto com os dados da requisição</param>
        /// <exception cref="ErroDeValidacaoException">Exceção lançada se a validação falhar</exception>
        private void Validador(ReservaDeEquipamentoRequest request)
        {
            // 1. Instancia o validador da request
            var validar = new ValidadorDereservaDeEquipamento();

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
        /// </summary>
        /// <param name="token">Token JWT enviado pelo cliente</param>
        public void ValidarToken(string token)
        {
            _servicoDeToken.ValidarToken(token);
        }
    }
}
