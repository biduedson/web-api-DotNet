

using Application.DTOs.Requests;
using Application.DTOs.Responses;

namespace Application.UseCases.CriarEquipamento;

public interface ICriarEquipamentoUseCase
{
    public Task<EquipamentoRegistradoResponse> Execute(RegistraEquipamentoRequest request);
}