using Application.DTOs.Requests;
using Application.DTOs.Responses;

namespace Application.UseCases.CriarUsuario;

/// <summary>
/// Interface que define o contrato para o caso de uso de criação de um novo usuário.
/// </summary>
/// <remarks>
/// Essa interface é implementada por uma classe que contém a lógica de negócio para registrar um novo usuário no sistema.
/// O método <c>Execute</c> deve receber os dados necessários para o registro e retornar uma resposta padronizada de sucesso.
/// </remarks>
public interface ICriarUsuarioUseCase
{
    /// <summary>
    /// Executa o processo de criação de um novo usuário com base nos dados fornecidos.
    /// </summary>
    /// <param name="request">Objeto que contém os dados necessários para registrar o usuário.</param>
    /// <returns>
    /// Um objeto do tipo <see cref="RespostaDeSucessoDaApi{Object}"/>, contendo informações sobre o sucesso da operação
    /// e os dados retornados, caso aplicável.
    /// </returns>
    Task<RespostaDeSucessoDaApi<Object>> Execute(RegistrarUsuarioRequest request);
}
