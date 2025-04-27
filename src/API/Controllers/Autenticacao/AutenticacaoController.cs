using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.Http;
using Application.Shared;
using Application.UseCases.Autenticacao;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

/// <summary>
/// Controller responsável por operações de autenticação de usuários.
/// </summary>
/// <remarks>
/// Define que essa classe é um controller de API RESTful.
/// Define também a rota base da API como "api/autenticacao".
/// </remarks>
[ApiController]
[Route("api/[controller]")]
public class AutenticacaoController : ControllerBase
{
    /// <summary>
    /// Dependência do caso de uso de autenticação de usuários.
    /// </summary>
    private readonly IAutenticacaoUseCase _autenticacaoUseCase;

    /// <summary>
    /// Construtor da controller que injeta o caso de uso de autenticação.
    /// </summary>
    /// <param name="autenticacaoUseCase">
    /// Caso de uso responsável por realizar a lógica de autenticação de usuários.
    /// </param>
    public AutenticacaoController(IAutenticacaoUseCase autenticacaoUseCase)
    {
        _autenticacaoUseCase = autenticacaoUseCase;
    }

    /// <summary>
    /// Realiza a autenticação do usuário com base nas credenciais fornecidas.
    /// </summary>
    /// <remarks>
    /// Esse método é acessado via requisição HTTP POST para o endpoint /api/autenticacao/autenticar.
    /// 
    /// Ele espera um corpo JSON contendo e-mail e senha do usuário, realiza a validação,
    /// e se as credenciais forem válidas, retorna um token JWT junto com dados do usuário autenticado.
    /// 
    /// O método retorna status HTTP 200 (OK) em caso de sucesso.
    /// </remarks>
    /// <param name="request">
    /// Objeto contendo o e-mail e a senha enviados no corpo da requisição.
    /// </param>
    /// <returns>
    /// Retorna uma resposta HTTP 200 OK com os dados do usuário autenticado,
    /// incluindo token de acesso e data de expiração.
    /// </returns>
    [HttpPost("autenticar")]
    [ProducesResponseType(typeof(RespostasDaApi<UsuarioAutenticado>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Autenticar([FromBody] AutenticacaoRequest request)
    {
        var usuarioAutenticado = await _autenticacaoUseCase.Execute(request);
        return Ok(usuarioAutenticado);
    }
}
