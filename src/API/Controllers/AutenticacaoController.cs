using Application.DTOs.Requests;
using Application.Shared;
using Application.UseCases.autenticacao;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AutenticacaoController : ControllerBase
{
    private readonly IAutenticacaoUseCase _autenticacaoUseCase;

    public AutenticacaoController(IAutenticacaoUseCase autenticacaoUseCase)
    {
        _autenticacaoUseCase = autenticacaoUseCase;
    }

    [HttpPost("autenticar")]
    [ProducesResponseType(typeof(RespostaDeSucessoDaApi<UsuarioAutenticado>),StatusCodes.Status200OK)]
    public async Task<IActionResult> Autenticar([FromBody] AutenticacaoRequest request)
    {
      var usuarioAutenticado = await _autenticacaoUseCase.Execute(request);
      return Ok(usuarioAutenticado);
    }
}