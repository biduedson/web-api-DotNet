using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.UseCases.CriarUsuario;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Controller responsável por lidar com operações relacionadas a usuários.
    /// Define a rota base da API: nesse caso, a URL será /api/usuarios
    /// Indica que essa classe é um controller de API e que o comportamento padrão será RESTful
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class Usuariocontroller : ControllerBase
    {
        /// <summary>
        /// Cria um novo usuário com base nos dados fornecidos no corpo da requisição.
        /// </summary>
        /// <remarks>
        /// Define que este método responde a requisições HTTP POST (inserção de dados)
        /// Documenta o tipo de resposta esperada para esse endpoint.
        /// Essa anotação é utilizada pelo Swagger/OpenAPI para indicar que este endpoint retorna:
        /// - Um objeto do tipo RespostaDeSucessoDaApi&lt;Object&gt; no corpo da resposta
        /// - E que o status HTTP da resposta será 201 (Created)
        /// 
        /// typeof(RespostaDeSucessoDaApi&lt;Object&gt;) apenas indica o tipo da classe retornada,
        /// sem criar uma instância — é usado apenas para documentação e análise estática.
        /// 
        /// Isso ajuda ferramentas como Swagger a gerar uma documentação mais precisa
        /// e também melhora a legibilidade da API para desenvolvedores.
        /// </remarks>
        /// <param name="useCase">
        /// [FromServices] injeta automaticamente o caso de uso (use case) da criação de usuário
        /// </param>
        /// <param name="request">
        /// [FromBody] indica que os dados do usuário virão no corpo da requisição em JSON
        /// </param>
        /// <returns>
        /// Retorna uma resposta HTTP 201 Created com o objeto de resultado no corpo da resposta.
        /// O primeiro parâmetro da função Created seria a URL de onde acessar esse recurso criado,
        /// mas como não estamos retornando a URL aqui, passamos string.Empty.
        /// </returns>
        [HttpPost]
        [ProducesResponseType(typeof(RespostaDeSucessoDaApi<Object>), StatusCodes.Status201Created)]
        public async Task<IActionResult> Post(
            [FromServices] ICriarUsuarioUseCase useCase,
            [FromBody] RegistrarUsuarioRequest request)
        {
            var result = await useCase.Execute(request);
            return Created(string.Empty, result);
        }
    }
}
