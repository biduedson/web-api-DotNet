using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.Shared;

namespace Application.UseCases.Autenticacao
{
    /// <summary>
    /// Interface responsável pelo caso de uso de autenticação de usuário.
    /// </summary>
    public interface IAutenticacaoUseCase
    {
        /// <summary>
        /// Método responsável por executar o processo de autenticação de um usuário.
        /// </summary>
        /// <param name="request">Objeto que contém os dados de autenticação, como email e senha.</param>
        /// <returns>Retorna um objeto de sucesso da API contendo os dados do usuário autenticado.</returns>
        Task<RespostaDeSucessoDaApi<UsuarioAutenticado>> Execute(AutenticacaoRequest request);
    }
}
