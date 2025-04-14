using System.Net;
using Application.DTOs.Responses.Error;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


// Define o namespace onde a classe está localizada.
namespace API.Filtros
{
    // Filtro global de exceções para capturar erros e retornar respostas padronizadas
    public class FiltroDeException : IExceptionFilter
    {
   // Método invocado automaticamente quando uma exceção ocorre durante o processamento de uma requisição.
    public void OnException(ExceptionContext context)
    {
   // Verifica se a exceção lançada faz parte das exceções personalizadas do projeto.
    if(context.Exception is DomainException)  
      TratarExcecaoDoProjeto(context);                                    // Trata a exceção específica do projeto.
       
    else  
      TratarExcecaoDesconhecida(context);                                // Trata exceções desconhecidas.
    }
   
     // Método responsável por lidar com exceções personalizadas do projeto.
   private void TratarExcecaoDoProjeto(ExceptionContext context)
   {
     // Verifica se a exceção é do tipo ErroDeValidacaoException (erro de validação).
     if(context.Exception is ErroDeValidacaoException)
     {
      // Converte a exceção para o tipo ErrorOnValidationException.
      var exception = context.Exception as ErroDeValidacaoException;

      // Define o código de status HTTP como 400 (Bad Request).
      context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

      // Retorna um objeto de erro contendo a lista de mensagens da exceção.
      context.Result = new BadRequestObjectResult(new RespostasDeErro(exception.MenssagensDeErro));
     }
     
   }

   // Método responsável por lidar com exceções desconhecidas.
   private void TratarExcecaoDesconhecida(ExceptionContext context)
   {
    // Define o código de status HTTP como 500 (Internal Server Error).
    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

    // Retorna um objeto de erro com uma mensagem genérica de erro desconhecido.
    context.Result = new ObjectResult(new RespostasDeErro("Erro desconhecido."));
   }
 }
}