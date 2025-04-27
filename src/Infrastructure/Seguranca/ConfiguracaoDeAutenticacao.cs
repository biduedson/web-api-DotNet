using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;

namespace Infrastructure.Seguranca
{
    /// <summary>
    /// Classe estática para configuração da autenticação JWT em aplicações ASP.NET Core.
    /// Fornece métodos para registrar e configurar serviços de autenticação baseada em tokens.
    /// </summary>
    public static class AddAutenticacaoJwt
    {
        /// <summary>
        /// Configura os serviços de autenticação JWT, incluindo validação de token e tratamento de eventos.
        /// </summary>
        /// <param name="services">A coleção de serviços onde a autenticação JWT será registrada.</param>
        /// <param name="configuration">A configuração da aplicação que contém as definições JWT na seção "Jwt".</param>
        /// <remarks>
        /// Este método configura:
        /// - Registro das configurações JWT para injeção de dependência
        /// - Esquema de autenticação JWT Bearer
        /// - Parâmetros de validação de token
        /// - Tratadores personalizados para eventos de autenticação
        /// </remarks>
        public static void Configurar(IServiceCollection services, IConfiguration configuration)
        {
            // Registra a seção "Jwt" do arquivo de configuração para acesso tipado via injeção de dependência
            services.Configure<ConfiguracaoJwt>(configuration.GetSection("Jwt"));

            // Configura o serviço de autenticação com o esquema JWT Bearer
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    // Define os parâmetros para validação do token JWT (assinatura, emissor, audiência, expiração, etc.)
                    options.TokenValidationParameters = ParametrosDeValidacaoDeToken.Parametros(services, configuration);

                    // Configura tratadores personalizados para diferentes eventos de autenticação
                    options.Events = new JwtBearerEvents
                    {
                        // Tratador para quando o token não é fornecido ou está mal formatado
                        OnChallenge = context => TratadorDeTokenInvalido.ExecutarAsync(context),

                        // Tratador para quando o usuário não tem permissão para acessar o recurso
                        OnForbidden = context => TratadorDeAcessoNegado.ExecutarAsync(context),

                        // Tratador para falhas de autenticação (token expirado, assinatura inválida, etc.)
                        OnAuthenticationFailed = context => TratadorDeFalhaNaAutenticacao.ExecutarAsync(context)
                    };
                });
        }
    }
}