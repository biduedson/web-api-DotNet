// Importa os DTOs (Data Transfer Objects) usados para entrada e saída de dados da API
using Application.DTOs;
using Application.DTOs.Requests;

// Importa o caso de uso responsável por criar equipamentos
using Application.UseCases.CriarEquipamento;

// Importa recursos do ASP.NET Core necessários para criação do controller
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    // Define a rota base da API: nesse caso, a URL será /api/equipamentos
    [Route("api/[controller]")]

    // Indica que essa classe é um controller de API e que o comportamento padrão será RESTful
    [ApiController]
    public class EquipamentosController : ControllerBase
    {
        // Define que este método responde a requisições HTTP POST (inserção de dados)
        [HttpPost]

        // Documenta o tipo de resposta esperada para esse endpoint

        // Essa anotação é utilizada pelo Swagger/OpenAPI para indicar que este endpoint retorna:
        // - Um objeto do tipo EquipamentoRegistradoResponse no corpo da resposta
        // - E que o status HTTP da resposta será 201 (Created)
        //
        // typeof(EquipamentoRegistradoResponse) apenas indica o tipo da classe retornada,
        // sem criar uma instância — é usado apenas para documentação e análise estática
        //
        // Isso ajuda ferramentas como Swagger a gerar uma documentação mais precisa
        // e também melhora a legibilidade da API para desenvolvedores
        [ProducesResponseType(typeof(RespostaDeSucessoDaApi<Object>), StatusCodes.Status201Created)]

        // Esse método é assíncrono e recebe dois parâmetros:
        // - [FromServices] injeta automaticamente o caso de uso (use case) da criação de equipamento
        // - [FromBody] indica que os dados do equipamento virão no corpo da requisição em JSON
        public async Task<IActionResult> Post(
            [FromServices] ICriarEquipamentoUseCase usecase,
            [FromBody] RegistraEquipamentoRequest request)
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
