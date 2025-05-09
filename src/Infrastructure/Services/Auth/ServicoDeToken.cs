using System.IdentityModel.Tokens.Jwt; // Biblioteca para trabalhar com tokens JWT.
using System.Security.Claims; // Biblioteca que define "Claims" (informações sobre o usuário).
using System.Text; // Usada para manipulação de strings e codificação.
using Application.Services.Token; // Serviço de tokens definido na aplicação.
using Domain;
using Domain.Exceptions;
using Microsoft.Extensions.Configuration; // Para acessar as configurações do aplicativo (appsettings).
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens; // Biblioteca para trabalhar com autenticação de tokens.

namespace Infrastructure.Services.Auth; // Define o namespace para a infraestrutura de autenticação.

public class ServicoDeToken : IServicoDeToken
{
    private readonly ConfiguracaoJwt _config; // Armazena a configuração do aplicativo, como a chave do JWT.

    /// <summary>
    /// Construtor que inicializa a configuração do serviço de token.
    /// Este construtor injeta as configurações necessárias (como chave secreta, emissor, e audiência) 
    /// a partir do arquivo de configuração ou variáveis de ambiente.
    /// </summary>
    /// <param name="config">Configuração das propriedades do JWT (como chave, emissor, e audiência).</param>
    public ServicoDeToken(IOptions<ConfiguracaoJwt> config)
    {
        _config = config.Value; // Inicializa a configuração com os dados do appsettings.json ou variáveis de ambiente.
    }

    /// <summary>
    /// Método para gerar um token JWT.
    /// Este método cria um token JWT contendo informações sobre o usuário, como ID, e-mail e papel (role),
    /// e assina o token com uma chave simétrica configurada na aplicação.
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
    /// Método para extrair o ID do usuário a partir do token JWT.
    /// Este método tenta decodificar o token JWT e buscar o "claim" que contém o ID do usuário.
    /// Se o token for válido, retorna o ID do usuário contido nele.
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

        // Caso não encontre o claim ou o valor não seja um GUID válido, retorna null.
        return null;
    }

    /// <summary>
    /// Extrai a data de expiração de um token JWT sem validá-lo.
    /// Este método apenas decodifica o token para acessar a data de expiração, mas não valida o token.
    /// </summary>
    /// <param name="token">Token JWT no formato compactado (JWS ou JWE).</param>
    /// <returns>Data e hora de expiração do token em UTC.</returns>
    /// <remarks>
    /// Este método utiliza o <see cref="JwtSecurityTokenHandler.ReadJwtToken(string)"/> para decodificar o token e acessar a propriedade <see cref="JwtSecurityToken.ValidTo"/> 
    /// que representa a data de expiração do token. É importante notar que este método não realiza a validação do token.
    /// Para garantir a integridade e validade do token, utilize o método <see cref="JwtSecurityTokenHandler.ValidateToken(string, TokenValidationParameters, out SecurityToken)"/>.
    /// </remarks>
    public DateTime ObterDataExpiracaoToken(string token)
    {
        // Instancia o manipulador de tokens JWT.
        var handler = new JwtSecurityTokenHandler();

        // Lê o token JWT sem validá-lo.
        var jwtToken = handler.ReadJwtToken(token);

        // Retorna a data de expiração do token (campo 'exp'), em UTC.
        return jwtToken.ValidTo;
    }

    /// <summary>
    /// Método responsável por obter o tipo de usuario (se e usuario comum ou admistrador.).
    /// </summary>
    /// <param name="token">Token JWT.</param>
    /// <returns>A data de expiração do token.</returns>
    public string ObterTipoDeUsuario(string token)
    {
        var handler = new JwtSecurityTokenHandler();

        var jwtToken = handler.ReadJwtToken(token);

        return jwtToken.Claims.FirstOrDefault(usuario => usuario.Type == "role")?.Value!;
    }
}
