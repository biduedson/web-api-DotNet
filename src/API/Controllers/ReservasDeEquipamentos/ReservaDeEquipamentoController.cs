using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.UseCases.ReservaDeEquipamentoUseCase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Controlador responsável por gerenciar operações relacionadas às reservas de equipamentos.
    /// </summary>
    [Authorize] // - Executa  uma Validacao do token 
    [Route("api/[controller]")] // Define a rota base da API: /api/ReservaDeEquipamentos
    [Controller] // Indica que essa classe é um controller de API com comportamento RESTful
    public class ReservaDeEquipamentosController : ControllerBase
    {
        /// <summary>
        /// Endpoint responsável por registrar um novo equipamento.
        /// </summary>
        /// <param name="usecase">Caso de uso responsável pela lógica de reserva de equipamento.</param>
        /// <param name="request">Dados da reserva de equipamento enviados no corpo da requisição.</param>
        /// <returns>Retorna uma resposta HTTP 201 Created com os dados da reserva de equipamento.</returns>

        /// <summary>
        /// - Executa o caso de uso para criar a reserva de equipamento.
        /// - Retorna uma resposta HTTP 201 com os dados da reserva criada.
        /// </summary>
        [HttpPost] // Define que este método responde a requisições HTTP POST (inserção de dados)
        [ProducesResponseType(typeof(RespostaDeSucessoDaApi<Object>), StatusCodes.Status201Created)] // Indica a resposta e o status HTTP 201
        public async Task<IActionResult> Post(
            [FromServices] ICriarReservaDeEquipamentoUseCase usecase, // Injeta o caso de uso responsável pela criação de reserva de equipamento
            [FromBody] ReservaDeEquipamentoRequest request // Dados da reserva de equipamento no corpo da requisição
        )
        {
            // Executa o caso de uso passando o request (com os dados da nova reserva de equipamento)
            var result = await usecase.Execute(request);
            // Retorna uma resposta HTTP 201 Created com o objeto de resultado no corpo da resposta
            // O primeiro parâmetro da função Created seria a URL de onde acessar esse recurso criado,
            // mas como não estamos retornando a URL aqui, passamos string.Empty
            return Created(string.Empty, result);
        }
    }
}
