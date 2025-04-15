// Importa o contexto do banco de dados (AppDbContext), que representa as entidades e as tabelas do banco
using Domain.Repositories.EquipamentoRepository;
using Domain.Repositories.UsuarioRepository;
using Infrastructure.Data.Context;
using Infrastructure.Data.Repositories;


// Importa os recursos do Entity Framework Core necessários para configurar o banco de dados
using Microsoft.EntityFrameworkCore;

// Importa a interface IConfiguration, usada para acessar configurações da aplicação (como strings de conexão)
using Microsoft.Extensions.Configuration;

// Importa a interface IServiceCollection, que permite registrar serviços na injeção de dependência
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    // Define uma classe estática chamada InjecaoDeDependencia.
    // Ela serve para organizar as configurações da camada de infraestrutura, como acesso a banco de dados.
    public static class InjecaoDeDependencia
    {
        /// <summary>
        /// Este método é uma extensão da IServiceCollection.
        /// Ele registra os serviços da camada de Infrastructure (como o AppDbContext) no container de injeção de dependência.
        /// </summary>
        /// <param name="services">É a coleção de serviços da aplicação, usada para registrar dependências.</param>
        /// <param name="configuration">É o objeto que carrega as configurações da aplicação (como appsettings.json).</param>
        public static void AdicionarInfrastructure(this IServiceCollection services, IConfiguration configuration)
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

            // Cria a versão do servidor MySQL que será usada pelo EF Core (aqui ainda sem especificar, você pode definir a versão exata)
            var serverVersion = new MySqlServerVersion(new Version());

            /*
            🔧 services.AddDbContext<AppDbContext>(...)
            - Registra o AppDbContext na injeção de dependência como um serviço.
            - Toda vez que alguém solicitar um AppDbContext via construtor, o .NET irá fornecer uma instância configurada.

            ⚙️ dbContextOptions =>
            - Lambda que permite configurar as opções do contexto.
            - Aqui usamos para dizer qual banco de dados usar e qual string de conexão utilizar.
            */
            services.AddDbContext<AppDbContext>(dbContextOptions =>
            {
                // Configura o AppDbContext para usar o MySQL com a string de conexão e a versão do servidor
                dbContextOptions.UseMySql(connectionString, serverVersion);
            });
           // Aqui registramos os repositórios na injeção de dependência:
           // ✅ O que significa AddScoped?
           // - Significa que a instância da classe será criada **uma única vez por requisição HTTP**.
           // - Durante uma mesma requisição, todos os lugares que solicitarem esse serviço (via construtor, por exemplo)
           //   receberão **a mesma instância**.
           // - Mas em uma nova requisição, será criada **uma nova instância**.
           //
           // Esse comportamento é ideal para serviços que usam DbContext, por exemplo, onde você quer manter
           // a mesma conexão/transação durante a requisição toda.

           // Exemplo: Se um controller usa ICriarUsuarioUseCase, e esse use case usa IUsuarioRepository,
           // ambos compartilharão a mesma instância durante aquela requisição.
             services.AddScoped<IUsuarioRepository, UsuarioRepository>();
             services.AddScoped<IEquipamentoRepository, EquipamentoRepository>();
        }
    }
}
