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
    /// Implementa tratamento centralizado de diferentes tipos de exceções na aplicação.
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
            else if (!context.ModelState.IsValid)
                TratarExcecaoDeTipo(context);
            else
                TratarExcecaoDesconhecida(context); // Trata exceções desconhecidas.
        }

        /// <summary>
        /// Método responsável por lidar com exceções personalizadas do projeto.
        /// Trata especificamente exceções de validação e autenticação.
        /// </summary>
        /// <param name="context">Contexto da exceção personalizada.</param>
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
        /// Método responsável por lidar com exceções desconhecidas não mapeadas.
        /// Retorna um erro genérico de servidor interno.
        /// </summary>
        /// <param name="context">Contexto da exceção desconhecida.</param>
        private void TratarExcecaoDesconhecida(ExceptionContext context)
        {
            // Define o código de status HTTP como 500 (Internal Server Error).
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            // Retorna um objeto de erro com uma mensagem genérica de erro desconhecido.
            context.Result = new ObjectResult(new RespostasDeErro("Erro desconhecido."));
        }

        /// <summary>
        /// Método para processar erros de validação de modelo com detalhamento completo.
        /// Extrai informações pormenorizadas sobre os campos com erro na requisição.
        /// </summary>
        private void TratarExcecaoDeTipo(ExceptionContext context)
        {
            // Define o código de status HTTP como 400 (Bad Request) para indicar erro na requisição
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            /// <summary>
            /// Processo de extração e mapeamento dos erros de validação:
            /// 1. Filtra o ModelState para encontrar campos com erros
            /// 2. Exclui campos com chave "request"
            /// 3. Transforma os erros em uma coleção de objetos com informações detalhadas
            /// </summary>
            var erros = context.ModelState
                // Filtra apenas os campos que têm erros e não são "request"
                .Where(e => e.Value!.Errors.Count > 0 && e.Key != "request")

                // Transforma cada erro em um objeto com informações específicas
                .SelectMany(e => e.Value!.Errors.Select(erro => new
                {
                    // Extrai o nome do campo, considerando propriedades aninhadas
                    // Se o campo contém '.', pega apenas a última parte (ex: "Endereco.Rua" -> "Rua")
                    Campo = e.Key.Contains('.') ? e.Key.Split('.').Last() : e.Key,

                    // Classifica o tipo de erro baseado na mensagem
                    // Se contém "required", classifica como "Campo ausente"
                    // Caso contrário, classifica como "Tipo inválido"
                    TipoErro = erro.ErrorMessage.ToLower().Contains("required")
                        ? "Campo ausente"
                        : "Tipo inválido"
                }))
                // Converte o resultado para uma lista
                .ToList();

            // Cria uma resposta de erro estruturada com:
            // - Mensagem genérica de erro
            // - Lista detalhada dos campos com erro
            context.Result = new ObjectResult(new
            {
                Mensagem = "Erro de validação na requisição.",
                CamposComErro = erros
            });
        }

        /// <summary>
        /// Método para tratar falhas de autenticação (401 Unauthorized) geradas automaticamente pelo ASP.NET Core.
        /// Converte a exceção para um formato padrão de resposta de erro.
        /// </summary>
        /// <param name="context">Contexto da exceção de autenticação.</param>
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
        /// Converte a exceção para um formato padrão de resposta de erro.
        /// </summary>
        /// <param name="context">Contexto da exceção de autorização.</param>
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