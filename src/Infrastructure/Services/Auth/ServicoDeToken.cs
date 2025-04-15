using System.IdentityModel.Tokens.Jwt; // Biblioteca para trabalhar com tokens JWT.
using System.Security.Claims; // Biblioteca que define "Claims" (informações sobre o usuário).
using System.Text; // Usada para manipulação de strings e codificação.
using Application.Services.Token; // Serviço de tokens definido na aplicação.
using Microsoft.Extensions.Configuration; // Para acessar as configurações do aplicativo (appsettings).
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens; // Biblioteca para trabalhar com autenticação de tokens.

namespace Infrastructure.Services.Auth; // Define o namespace para a infraestrutura de autenticação.

public class ServicoDeToken : IServicoDeToken
{
    private readonly ConfiguracaoJwt _config; // Armazena a configuração do aplicativo, como a chave do JWT.

    // Construtor que injeta as configurações do app.
    public ServicoDeToken(IOptions<ConfiguracaoJwt> config)
    {
        _config = config.Value; // Inicializa a configuração com os dados do appsettings.json.
    }

    /// <summary>
    /// Método para gerar um token JWT.
    /// </summary>
    /// <param name="idUsuario">ID único do usuário (GUID).</param>
    /// <param name="email">E-mail do usuário.</param>
    /// <param name="administrador">Flag indicando se o usuário é administrador.</param>
    /// <returns>Token JWT como uma string.</returns>
    public string GerarToken(Guid idUsuario, string email, bool administrador)
    {
        // Definindo os "claims" (declarações) do token. Claims são informações sobre o usuário.
        var claims = new[]
        {
            // Claim "sub" (Subject): Identificador único do usuário (ID).
            new Claim(JwtRegisteredClaimNames.Sub, idUsuario.ToString()),

            // Claim "email": O e-mail do usuário, que é um tipo de claim pré-definido.
            new Claim(ClaimTypes.Email, email),

            // Claim "role": Função do usuário (Administrador ou Usuário Comum).
            new Claim(ClaimTypes.Role, administrador ? "Administrador" : "UsuarioComun"),
        };

        // Criando a chave simétrica para assinar o token. A chave vem das configurações do app.
        var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Key));

        // Definindo as credenciais de assinatura do token, usando o algoritmo HMACSHA256.
        var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

        // Criando o token JWT com as informações definidas (claims, expiração, assinatura).
        var token = new JwtSecurityToken(
            issuer: _config.Issuer, // O emissor do token (geralmente o domínio da aplicação).
            audience: _config.Audience, // A audiência do token, que define quem pode aceitar esse token.
            claims: claims, // Informações sobre o usuário (os "claims").
            expires: DateTime.UtcNow.AddHours(_config.ExpiresInHours), // O token expirará em 1 hora.
            signingCredentials: credenciais // As credenciais para assinar o token.
        );

        // Retorna o token JWT como uma string compactada.
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    /// <summary>
    /// Método para validar se um token JWT é válido.
    /// </summary>
    /// <param name="token">Token JWT a ser validado.</param>
    /// <returns>True se o token for válido, false caso contrário.</returns>
    public bool ValidarToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler(); // Criando o manipulador para processar o token.
        
        // Recuperando a chave usada para assinar o token (do arquivo de configuração).
        var chave = Encoding.UTF8.GetBytes(_config.Key);

        try
        {
            // Tentando validar o token usando as configurações de validação.
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true, // Valida o emissor do token para garantir que é de uma fonte confiável.
                ValidateAudience = true, // Valida a audiência do token (quem deve aceitar esse token).
                ValidAudience = _config.Issuer, // A audiência válida é a mesma configurada no appsettings.
                IssuerSigningKey = new SymmetricSecurityKey(chave), // A chave usada para validar a assinatura do token.
                ValidateLifetime = true, // Verifica se o token não expirou.
                ClockSkew = TimeSpan.Zero // Ajuste de tempo para verificação de expiração (sem margem de erro).
            }, out SecurityToken validatedToken); // O token validado é retornado aqui se for válido.

            return true; // Se o token for validado com sucesso, retorna true.
        }
        catch
        {
            return false; // Caso ocorra qualquer erro, o token é considerado inválido e retorna false.
        }
    }

    /// <summary>
    /// Método para extrair o ID do usuário a partir do token JWT.
    /// </summary>
    /// <param name="token">Token JWT que contém o ID do usuário.</param>
    /// <returns>O ID do usuário se o token for válido, caso contrário retorna null.</returns>
    public Guid? ObterIdDoUsuarioDoToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler(); // Criando o manipulador de tokens JWT.

        // Lendo o token JWT para acessá-lo.
        var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

        // Buscando o "claim" que contém o ID do usuário (sub).
        var idclaim = jwtToken?.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);

        // Se o claim de ID existir e for um GUID válido, retorna o ID do usuário.
        if (idclaim != null && Guid.TryParse(idclaim.Value, out var id))
        {
            return id;
        }

        return null; // Caso contrário, retorna null (token inválido ou sem ID).
    }
}
