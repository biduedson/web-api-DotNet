using System.Reflection;
using Microsoft.OpenApi.Models;

namespace API.Dependencias
{
    public static class ConfigurarSwagger
    {
        /// <summary>
        /// Configura o Swagger para documentação da API
        /// </summary>
        /// <param name="services">Coleção de serviços onde a configuração do Swagger será adicionada</param>
        public static void AdicionarSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                /// <summary>
                /// Configura o Swagger para fornecer informações sobre a API, como título, versão e descrição
                /// </summary>
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Reserva de equipamentos API",
                    Version = "v1",
                    Description = "Documentação da API"
                });

                /// <summary>
                /// Habilita comentários XML para as descrições de métodos, parâmetros, etc.
                /// </summary>
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                /// <summary>
                /// Configuração para suportar autenticação JWT no Swagger
                /// </summary>
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Insira o token JWT desta maneira: Bearer {seu token}"
                });

                /// <summary>
                /// Adiciona o requisito de autenticação para todas as requisições
                /// </summary>
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });
        }
    }
}
