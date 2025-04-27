using System.Net;
using Application.DTOs.Responses.Error;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filtros.Exceptions
{
    /// <summary>
    /// Estratégia de tratamento para exceções de validação
    /// </summary>
    public class TratadorExceptionDeRequisicao : ITratamentoDeEcxeption
    {
        public bool PodeTratar(Exception exception) => exception is ErroDeValidacaoException;

        public void Tratar(ExceptionContext context)
        {
            // Define o código de status HTTP como 400 (Bad Request) para indicar erro na requisição
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            /// <summary>
            /// Processo de extração e mapeamento dos erros de validação:
            /// 1. Filtra o ModelState para encontrar campos com erros
            /// 2. Exclui campos com chave "request"
            /// 3. Transforma os erros em uma coleção de objetos com informações detalhadas
            /// </summary>

            var erro = context.ModelState
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
                    TipoErro = "Campo com dado inválido ou ausente."
                }));


            // Cria uma resposta de erro estruturada com:
            // - Mensagem genérica de erro
            // - Lista detalhada dos campos com erro
            context.Result = new ObjectResult(new
            {
                Mensagem = "Erro de validação na requisição.",
                CamposComErro = erro
            });

        }
    }
}