using System.Net;
using Application.DTOs.Responses.Error;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filtros.Exceptions
{
    public class TratadorDeExceptionDoProjeto : ITratamentoDeEcxeption
    {
        public bool PodeTratar(Exception exception) => exception is DomainException;

        public void Tratar(ExceptionContext context)
        {
            var exceptionDeDominio = context.Exception as ErroDeValidacaoException;

            // Define o código de status HTTP como 400 (Bad Request).
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            // Retorna um objeto de erro contendo a lista de mensagens da exceção.
            context.Result = new BadRequestObjectResult(new RespostasDeErro(exceptionDeDominio!.MenssagensDeErro));

        }
    }
}