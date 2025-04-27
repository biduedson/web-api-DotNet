using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.Http;

namespace Application.UseCases.CriarEquipamento
{
    /// <summary>
    /// Interface responsável pelo caso de uso de criação de um novo equipamento.
    /// </summary>
    public interface ICriarEquipamentoUseCase
    {
        /// <summary>
        /// Método responsável por criar um novo equipamento com base nos dados fornecidos.
        /// </summary>
        /// <param name="request">Objeto que contém os dados necessários para a criação de um equipamento.</param>
        /// <returns>Retorna um objeto de sucesso da API com os dados do equipamento criado.</returns>
        Task<IRespostasDaApi<Object>> Execute(RegistraEquipamentoRequest request);

    }
}
