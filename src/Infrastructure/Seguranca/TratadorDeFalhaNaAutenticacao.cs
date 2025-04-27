using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Seguranca;

/// <summary>
/// Classe utilitária para tratar eventos de falha na autenticação JWT.
/// Fornece métodos para personalizar a resposta quando ocorre uma falha durante a validação do token.
/// </summary>
public static class TratadorDeFalhaNaAutenticacao
{
    /// <summary>
    /// Executa o tratamento de eventos de falha na autenticação JWT.
    /// </summary>
    /// <param name="context">O contexto do evento de falha na autenticação.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona de escrita da resposta HTTP.</returns>
    /// <remarks>
    /// Este método:
    /// - Detecta o tipo específico de falha de autenticação
    /// - Para tokens expirados, retorna uma mensagem específica
    /// - Para outros erros, retorna uma mensagem genérica de token inválido
    /// - Define o código de status HTTP 401 (Unauthorized) em ambos os casos
    /// </remarks>
    public static Task ExecutarAsync(AuthenticationFailedContext context)
    {
        // Verifica se a exceção é de token expirado
        if (context.Exception is SecurityTokenExpiredException)
        {
            // Define o código de status HTTP 401 (Unauthorized)
            context.Response.StatusCode = 401;

            // Define o tipo de conteúdo da resposta como JSON
            context.Response.ContentType = "application/json";

            // Cria e serializa um objeto anônimo com a mensagem específica de token expirado
            var resposta = JsonSerializer.Serialize(new { erro = "Seu token expirou. Faça login novamente." });

            // Escreve a resposta JSON no corpo da resposta HTTP e retorna a tarefa resultante
            return context.Response.WriteAsync(resposta);
        }

        // Para qualquer outro tipo de falha na autenticação
        // Define o código de status HTTP 401 (Unauthorized)
        context.Response.StatusCode = 401;

        // Cria e serializa um objeto anônimo com uma mensagem genérica de token inválido
        var resultInvalidToken = JsonSerializer.Serialize(new { message = "Token inválido ou não fornecido." });

        // Escreve a resposta JSON no corpo da resposta HTTP e retorna a tarefa resultante
        return context.Response.WriteAsync(resultInvalidToken);
    }
}