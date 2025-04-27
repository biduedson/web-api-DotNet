using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Domain.Entities;

namespace Application.UseCases.ReservaDeEquipamentoUseCase
{
    /// <summary>
    /// Interface responsável pelo caso de uso de criação de uma reserva de equipamento.
    /// </summary>
    public interface ICriarReservaDeEquipamentoUseCase
    {
        /// <summary>
        /// Método responsável por criar uma nova reserva de equipamento com base nos dados fornecidos.
        /// </summary>
        /// <param name="request">Objeto que contém os dados necessários para a criação de uma reserva de equipamento.</param>
        /// <returns>Retorna um objeto de sucesso da API com os dados da reserva de equipamento criada.</returns>
        Task<RespostaDeSucessoDaApi<ReservaDeEquipamento>> Execute(ReservaDeEquipamentoRequest request);

    }
}
