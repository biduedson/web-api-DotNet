using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Seguranca;

/// <summary>
/// Classe utilitária para tratar eventos de acesso negado na autenticação JWT.
/// Fornece métodos para personalizar a resposta quando um usuário autenticado não tem permissão para acessar um recurso.
/// </summary>
public static class TratadorDeAcessoNegado
{
    /// <summary>
    /// Executa o tratamento de eventos de acesso negado durante a autenticação JWT.
    /// </summary>
    /// <param name="context">O contexto do evento de acesso negado (ForbiddenContext).</param>
    /// <returns>Uma tarefa que representa a operação assíncrona de escrita da resposta HTTP.</returns>
    /// <remarks>
    /// Este método:
    /// - Define o código de status HTTP 403 (Forbidden)
    /// - Define o tipo de conteúdo como application/json
    /// - Gera uma resposta JSON padronizada com uma mensagem de erro
    /// </remarks>
    public static Task ExecutarAsync(ForbiddenContext context)
    {
        // Define o código de status HTTP 403 (Forbidden)
        context.Response.StatusCode = 403;

        // Define o tipo de conteúdo da resposta como JSON
        context.Response.ContentType = "application/json";

        // Cria e serializa um objeto anônimo com a mensagem de erro
        var resposta = JsonSerializer.Serialize(new { erro = "Você não tem permissão para acessar este recurso." });

        // Escreve a resposta JSON no corpo da resposta HTTP e retorna a tarefa resultante
        return context.Response.WriteAsync(resposta);
    }
}