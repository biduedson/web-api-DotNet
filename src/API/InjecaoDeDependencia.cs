using System.Data.SqlTypes;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace API
{
    /// <summary>
    /// Classe estática para configuração de serviços específicos da API
    /// </summary>
    public static class InjecaoDeDependencia
    {
        /// <summary>
        /// Método de extensão para adicionar serviços específicos da API
        /// </summary>
        /// <param name="services">Coleção de serviços</param>
        /// <param name="configuration">Configuração da aplicação</param>
        public static void AdicionarServicosDaAPI(this IServiceCollection services, IConfiguration configuration)
        {
            // Configuração do Swagger
            ConfigurarSwagger(services);

            // Configuração de CORS
            AdicionarServicosCors(services);

            // Configuração de ModelState
            AlterarValidacaoDeModelo(services);

        }

        /// <summary>
        /// Configura o Swagger para doo comportamento padrão de validação de modelo no ASP.NETCORE
        /// Especificamente, SuppressModelStateInvalidFilter = true desabilita o comportamento padrão
        ///  de retornar automaticamente um BadRequest quando o ModelState não é válido.
        /// Pois esta apliocação ja tem um metodo manul de filtro de exceptions que trata esse tipo de validaçaão.
        /// </summary> 
        public static void AlterarValidacaoDeModelo(IServiceCollection services)
        {
            /// <summary>
            /// desabilita o comportamento padrão de retornar automaticamente um BadRequest quando o ModelState não é válido.
            /// </summary> 
            services.Configure<ApiBehaviorOptions>(options =>
                   {
                       options.SuppressModelStateInvalidFilter = true;
                   });
        }

        /// <summary>
        /// Configura o Swagger para documentação da API
        /// </summary> 
        private static void ConfigurarSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Reserva de equipamentos API",
                    Version = "v1",
                    Description = "Documentação da API"
                });

                // Habilita comentários XML
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                // Configuração para suportar autenticação JWT no Swagger
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Insira o token JWT desta maneira: Bearer {seu token}"
                });
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
                        new string[] {}
                    }
                    });
            });
        }

        /// <summary>
        /// Configura a política de CORS
        /// </summary>
        private static void AdicionarServicosCors(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("PoliticaCORS", builder =>
                 {
                     builder
                     .AllowAnyOrigin()
                     .AllowAnyMethod()
                     .AllowAnyHeader();
                 });
            });
        }
    }
}