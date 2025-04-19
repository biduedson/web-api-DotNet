using System.Net;
using Application.DTOs.Responses.Error;
using Domain;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filtros
{
    /// <summary>
    /// Filtro global de exceções para capturar erros e retornar respostas padronizadas.
    /// </summary>
    public class FiltroDeException : IExceptionFilter
    {
        /// <summary>
        /// Método invocado automaticamente quando uma exceção ocorre durante o processamento de uma requisição.
        /// - Verifica se a exceção é uma exceção personalizada do projeto ou uma exceção desconhecida.
        /// - Chama os métodos específicos de tratamento para cada tipo de exceção.
        /// </summary>
        /// <param name="context">Contexto da exceção.</param>
        public void OnException(ExceptionContext context)
        {
            // Verifica se a exceção lançada faz parte das exceções personalizadas do projeto.
            if (context.Exception is DomainException)
                TratarExcecaoDoProjeto(context); // Trata a exceção específica do projeto.
            else
                TratarExcecaoDesconhecida(context); // Trata exceções desconhecidas.
        }

        /// <summary>
        /// Método responsável por lidar com exceções personalizadas do projeto.
        /// - Verifica o tipo de exceção (ErroDeValidacaoException ou ErroDeAutenticacaoException).
        /// - Define o código de status HTTP adequado.
        /// - Retorna um objeto de erro com as mensagens apropriadas.
        /// </summary>
        /// <param name="context">Contexto da exceção.</param>
        private void TratarExcecaoDoProjeto(ExceptionContext context)
        {
            // Verifica se a exceção é do tipo ErroDeValidacaoException (erro de validação).
            if (context.Exception is ErroDeValidacaoException exceptionValidacao)
            {
                // Define o código de status HTTP como 400 (Bad Request).
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                // Retorna um objeto de erro contendo a lista de mensagens da exceção.
                context.Result = new BadRequestObjectResult(new RespostasDeErro(exceptionValidacao.MenssagensDeErro));
                return;
            }

            // Verifica se a exceção é do tipo ErroDeAutenticacaoException (erro de autenticação).
            if (context.Exception is ErroDeAutenticacaoException exceptionAutenticacao)
            {
                // Define o código de status HTTP como 401 (Unauthorized).
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

                // Retorna um objeto de erro contendo a mensagem da exceção.
                context.Result = new UnauthorizedObjectResult(new RespostasDeErro(exceptionAutenticacao.MenssagenDeErro));
                return;
            }
        }

        /// <summary>
        /// Método responsável por lidar com exceções desconhecidas.
        /// - Define o código de status HTTP como 500 (Internal Server Error).
        /// - Retorna um objeto de erro com uma mensagem genérica de erro desconhecido.
        /// </summary>
        /// <param name="context">Contexto da exceção.</param>
        private void TratarExcecaoDesconhecida(ExceptionContext context)
        {
            // Define o código de status HTTP como 500 (Internal Server Error).
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            // Retorna um objeto de erro com uma mensagem genérica de erro desconhecido.
            context.Result = new ObjectResult(new RespostasDeErro("Erro desconhecido."));
        }

        /// <summary>
        /// Método para tratar falhas de autenticação (401 Unauthorized) geradas automaticamente pelo ASP.NET Core.
        /// - Converte a exceção para o tipo ErroDeAutenticacaoException.
        /// - Retorna uma resposta de erro com a mensagem de acesso negado.
        /// </summary>
        /// <param name="context">Contexto da exceção.</param>
        private void TratarExcecaoDeAutenticacao(ExceptionContext context)
        {
            // Converte a exceção para o tipo ErroDeAutenticacaoException.
            var exception = context.Exception as ErroDeAutenticacaoException;

            // Define o código de status HTTP como 401 (Unauthorized).
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

            // Retorna um objeto de erro contendo a mensagem de acesso negado.
            context.Result = new ObjectResult(new RespostasDeErro(MensagensDeExceptionAutenticacao.ACESSO_NEGADO));
        }

        /// <summary>
        /// Método para tratar falhas de autorização (403 Forbidden).
        /// - Converte a exceção para o tipo ErroDeValidacaoException.
        /// - Retorna uma resposta de erro com a mensagem de acesso negado.
        /// </summary>
        /// <param name="context">Contexto da exceção.</param>
        private void TratarExcecaoDeForbiddenDoAspNetCore(ExceptionContext context)
        {
            // Converte a exceção para o tipo ErroDeValidacaoException.
            var exception = context.Exception as ErroDeValidacaoException;

            // Define o código de status HTTP como 403 (Forbidden).
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;

            // Retorna um objeto de erro contendo a mensagem de acesso negado.
            context.Result = new ObjectResult(new RespostasDeErro(MensagensDeExceptionAutenticacao.ACESSO_NEGADO));
        }
    }
}
