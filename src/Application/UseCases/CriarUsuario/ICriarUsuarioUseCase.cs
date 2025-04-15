
using Application.DTOs.Requests;

namespace Application.UseCases.CriarUsuario;

public interface ICriarUsuarioUseCase
{
   public Task<RespostaDeSucessoDaApi<Object>> Execute(RegistrarUsuarioRequest request);
}