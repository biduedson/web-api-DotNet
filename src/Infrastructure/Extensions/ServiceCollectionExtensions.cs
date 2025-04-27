// Importa o contexto do banco de dados (AppDbContext), que representa as entidades e as tabelas do banco

using Application.Services.Criptografia;
using Application.Services.Token;
using Domain.Repositories.EquipamentoRepository;
using Domain.Repositories.ReservaDeEquipamentosRepository;
using Domain.Repositories.UsuarioRepository;
using Infrastructure.Data.Context;
using Infrastructure.Data.Repositories;
using Infrastructure.Seguranca;
using Infrastructure.Services.Auth;
using Infrastructure.Services.Criptografia;

// Importa os recursos do Entity Framework Core necessários para configurar o banco de dados
using Microsoft.EntityFrameworkCore;

// Importa a interface IConfiguration, usada para acessar configurações da aplicação (como strings de conexão)
using Microsoft.Extensions.Configuration;

// Importa a interface IServiceCollection, que permite registrar serviços na injeção de dependência
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    // Define uma classe estática chamada InjecaoDeDependencia.
    // Ela serve para organizar as configurações da camada de infraestrutura, como acesso a banco de dados.
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Este método é uma extensão da IServiceCollection.
        /// Ele registra os serviços da camada de Infrastructure (como o AppDbContext) no container de injeção de dependência.
        /// </summary>
        /// <param name="services">É a coleção de serviços da aplicação, usada para registrar dependências.</param>
        /// <param name="configuration">É o objeto que carrega as configurações da aplicação (como appsettings.json).</param>
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            /*
            ❓ O que é `this IServiceCollection services`?
            - É uma **extensão de método**, ou seja, esse método será chamado como se fosse parte da IServiceCollection.
              Exemplo: builder.Services.AdicionarInfrastructure(...);

            🧠 `IServiceCollection`:
            - É uma interface fornecida pelo .NET que representa uma **coleção de serviços** que podem ser injetados via DI (Dependency Injection).
            - Usamos ela para registrar classes, interfaces, banco de dados, etc.

            🧠 `IConfiguration`:
            - Interface que representa as configurações da aplicação.
            - Pode acessar valores definidos em arquivos como `appsettings.json`, variáveis de ambiente, etc.
            - Aqui usamos para obter a **string de conexão do banco de dados**.
            */

            // Obtém a string de conexão chamada "Default" do arquivo de configuração (appsettings.json, por exemplo)
            var connectionString = configuration.GetConnectionString("Default");

            // Cria a versão do servidor MySQL que será usada pelo EF Core
            var serverVersion = new MySqlServerVersion(new Version());

            /*
            🔧 services.AddDbContext<AppDbContext>(...)
            - Registra o AppDbContext na injeção de dependência como um serviço.
            - Toda vez que alguém solicitar um AppDbContext via construtor, o .NET irá fornecer uma instância configurada.
            */
            services.AddDbContext<AppDbContext>(dbContextOptions =>
            {
                dbContextOptions.UseMySql(connectionString, serverVersion);
            });
            var jwtSection = configuration.GetSection("Jwt");
            services.Configure<ConfiguracaoJwt>(jwtSection);
            AddAutenticacaoJwt.Configurar(services, configuration);
            // Bind do bloco "Jwt" para ConfiguracoesJwt


            // Registro dos repositórios seguindo o padrão de abstração via interfaces
            // ✅ AddScoped: Uma instância será criada por requisição HTTP (ideal para uso com DbContext)
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IEquipamentoRepository, EquipamentoRepository>();
            services.AddScoped<IReservaDeEquipamentosRepository, ReservaDeEquipamentoRepository>();

            // Registro do serviço de token com tempo de vida Singleton
            services.AddSingleton<IServicoDeToken, ServicoDeToken>();




            // Chamada do método que registra o serviço de criptografia
            AdicionarCriptografiaDesenha(services, configuration);

        }

        /// <summary>
        /// Registra a dependência do serviço de criptografia de senha com injeção de configuração personalizada.
        /// </summary>
        private static void AdicionarCriptografiaDesenha(IServiceCollection services, IConfiguration configuration)
        {
            /*
            ✅ Registrando a dependência pela interface ICriptografiaDeSenha — padrão de abstração.

            🔁 Usando uma lambda (factory) porque precisa passar um parâmetro (AdditionalKey) que não está disponível diretamente no container.

            🧩 Garantindo que a injeção de dependência funcione mesmo com parâmetros externos (como configurações).
            */

            services.AddScoped<ICriptografiaDeSenha>(provider =>
            {
                // Obtém a chave adicional de criptografia das configurações
                var AdditionalKey = configuration.GetValue<string>("Settings:Passwords:AdditionalKey");
                // Retorna uma nova instância da implementação com os parâmetros necessários
                return new CriptografiaDeSenha(configuration, AdditionalKey!);
            });
        }
    }
}
