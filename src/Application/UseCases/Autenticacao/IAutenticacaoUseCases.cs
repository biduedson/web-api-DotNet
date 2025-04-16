using Application.DTOs.Requests;
using Application.Shared;

namespace Application.UseCases.autenticacao;

public interface IAutenticacaoUseCase
{
    public  Task<RespostaDeSucessoDaApi<UsuarioAutenticado>> Execute(AutenticacaoRequest request);
}