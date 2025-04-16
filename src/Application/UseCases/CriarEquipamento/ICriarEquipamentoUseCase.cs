
using Application.DTOs.Requests;

namespace Application.UseCases.CriarEquipamento;

public interface ICriarEquipamentoUseCase
{
    public Task<RespostaDeSucessoDaApi<Object>> Execute(RegistraEquipamentoRequest request);
    public void ValidarToken(string token);
}