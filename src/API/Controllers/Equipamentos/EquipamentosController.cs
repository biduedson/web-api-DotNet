// Importa os DTOs (Data Transfer Objects) usados para entrada e saída de dados da API
using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.Http;



// Importa o caso de uso responsável por criar equipamentos
using Application.UseCases.CriarEquipamento;
using Microsoft.AspNetCore.Authorization;


// Importa recursos do ASP.NET Core necessários para criação do controller
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Controlador responsável por gerenciar operações relacionadas aos equipamentos.
    /// </summary>
    [Route("api/[controller]")] // Define a rota base da API: /api/equipamentos
    [ApiController] // Indica que essa classe é um controller de API com comportamento RESTful
    public class EquipamentosController : ControllerBase
    {
        /// <summary>
        /// Endpoint responsável por registrar um novo equipamento.
        /// </summary>
        /// <param name="usecase">Caso de uso responsável pela lógica de criação de equipamento.</param>
        /// <param name="request">Dados do equipamento enviados no corpo da requisição.</param>
        /// <returns>Retorna uma resposta HTTP 201 Created com os dados do equipamento registrado.</returns>

        [Authorize]
        [HttpPost] // Define que este método responde a requisições HTTP POST (inserção de dados)

        // Indica ao Swagger/OpenAPI que este endpoint retorna:
        // - Um objeto do tipo RespostaDeSucessoDaApi<Object> no corpo da resposta
        // - E que o status HTTP da resposta será 201 (Created)
        [ProducesResponseType(typeof(RespostasDaApi<Object>), StatusCodes.Status201Created)]
        public async Task<IActionResult> Post(
            [FromServices] ICriarEquipamentoUseCase usecase, // Injeta o caso de uso responsável pela criação
            [FromBody] RegistraEquipamentoRequest request // Recebe os dados do equipamento via corpo da requisição
        )
        {
            // Executa o caso de uso passando o request (com os dados do novo equipamento)
            var result = await usecase.Execute(request);

            // Retorna uma resposta HTTP 201 Created com o objeto de resultado no corpo da resposta
            // O primeiro parâmetro da função Created seria a URL de onde acessar esse recurso criado,
            // mas como não estamos retornando a URL aqui, passamos string.Empty
            return Created(string.Empty, result);
        }
    }
}
