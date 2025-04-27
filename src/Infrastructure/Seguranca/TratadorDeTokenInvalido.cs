using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Seguranca;

/// <summary>
/// Classe utilitária para tratar eventos de desafio de autenticação quando um token JWT é inválido.
/// Fornece métodos para personalizar a resposta quando um token está ausente, mal formatado ou inválido.
/// </summary>
public static class TratadorDeTokenInvalido
{
    /// <summary>
    /// Executa o tratamento de eventos de desafio de autenticação para tokens inválidos.
    /// </summary>
    /// <param name="context">O contexto do evento de desafio de autenticação JWT.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona de escrita da resposta HTTP.</returns>
    /// <remarks>
    /// Este método:
    /// - Substitui a resposta padrão do middleware de autenticação
    /// - Define o código de status HTTP 401 (Unauthorized)
    /// - Define o tipo de conteúdo como application/json
    /// - Retorna uma mensagem de erro em formato JSON
    /// </remarks>
    public static Task ExecutarAsync(JwtBearerChallengeContext context)
    {
        // Informa ao middleware que iremos tratar a resposta manualmente
        context.HandleResponse(); //👈Desativa a resposta padrão

        // Define o código de status HTTP 401 (Unauthorized)
        context.Response.StatusCode = 401;

        // Define o tipo de conteúdo da resposta como JSON
        context.Response.ContentType = "application/json";

        // Cria e serializa um objeto anônimo com a mensagem de erro
        var resposta = JsonSerializer.Serialize(new { error = "Token inválido ou não fornecido." });

        // Escreve a resposta JSON no corpo da resposta HTTP e retorna a tarefa resultante
        return context.Response.WriteAsync(resposta);
    }
}