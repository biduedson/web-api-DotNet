using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Seguranca
{
    /// <summary>
    /// Classe utilitária para criação e configuração dos parâmetros de validação de tokens JWT.
    /// Fornece métodos para definir as regras de validação usadas na autenticação.
    /// </summary>
    public static class ParametrosDeValidacaoDeToken
    {
        /// <summary>
        /// Cria e configura os parâmetros de validação para tokens JWT com base nas configurações da aplicação.
        /// </summary>
        /// <param name="services">A coleção de serviços da aplicação.</param>
        /// <param name="configuration">A configuração da aplicação contendo as definições JWT na seção "Jwt".</param>
        /// <returns>Um objeto TokenValidationParameters configurado com as regras de validação.</returns>
        /// <remarks>
        /// Este método configura:
        /// - Validação do emissor (issuer)
        /// - Validação da audiência (audience)
        /// - Validação do tempo de vida do token
        /// - Validação da chave de assinatura
        /// - Tipo de claim para papéis/roles
        /// </remarks>
        public static TokenValidationParameters Parametros(IServiceCollection services, IConfiguration configuration)
        {
            // Obtém as configurações JWT da seção "Jwt" do arquivo de configuração
            var config = configuration.GetSection("Jwt").Get<ConfiguracaoJwt>();

            // Cria e retorna um novo objeto de parâmetros de validação de token com as configurações específicas
            return new TokenValidationParameters
            {
                // Ativa a validação do emissor do token
                ValidateIssuer = true,

                // Ativa a validação da audiência do token
                ValidateAudience = true,

                // Ativa a validação do tempo de vida do token (data de expiração)
                ValidateLifetime = true,

                // Ativa a validação da chave de assinatura usada para assinar o token
                ValidateIssuerSigningKey = true,

                // Define o emissor válido com base na configuração
                ValidIssuer = config!.Issuer,

                // Define a audiência válida com base na configuração
                ValidAudience = config.Audience,

                // Cria uma chave simétrica de segurança usando a chave secreta da configuração
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Key!)),

                // Define qual claim será usada para identificar papéis/roles do usuário
                RoleClaimType = "role"
            };
        }
    }
}