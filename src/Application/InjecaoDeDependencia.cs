// Importações necessárias para o uso do AutoMapper, configuração e injeção de dependência
using Application.UseCases.CriarEquipamento;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;

namespace Application
{
    // Classe estática usada para registrar todos os serviços da camada de aplicação.
    // É comum criarmos uma classe com esse nome (InjecaoDeDependencia ou DependencyInjection)
    // em cada camada (Application, Infra, WebAPI, etc.) para manter as responsabilidades separadas.
    public static class InjecaoDeDependencia
    {
        // Método de extensão que será chamado a partir do Program.cs
        // Ele recebe dois parâmetros:
        //
        // IServiceCollection services → é a lista de todos os serviços que sua aplicação conhece.
        // IConfiguration configuration → representa o arquivo appsettings.json, variáveis de ambiente e outras configs.
        
        //✅ O que é AddAutoMapper(typeof(...).Assembly)?
        //É um atalho que diz ao AutoMapper para procurar automaticamente todos os mapeamentos dentro de um assembly.
        //Toda classe que herda de Profile será registrada sem você precisar fazer manualmente.
        public static void AdicionarApplication(this IServiceCollection services, IConfiguration configuration)
        {
            // Registra os perfis do AutoMapper da camada Application
            AdicionarAutoMapper(services);

            // Registra os casos de uso da aplicação
            AdicionarUseCases(services);
        }

        // Este método adiciona o AutoMapper à injeção de dependência.
        // O AutoMapper é uma biblioteca que facilita o mapeamento entre objetos (por exemplo, DTO → Entidade).
        private static void AdicionarAutoMapper(IServiceCollection services)
        {
            // Essa forma registra automaticamente todos os perfis de mapeamento (classes que herdam de Profile)
            // localizados no mesmo assembly (projeto) da classe InjecaoDeDependencia.
            //
            // Isso evita que você tenha que adicionar um por um (ex: options.AddProfile(new MeuProfile()))
            services.AddAutoMapper(typeof(InjecaoDeDependencia).Assembly);
        }

        // Aqui registramos os casos de uso (use cases), que são as regras de negócio da aplicação.
        // Cada use case é uma classe que executa uma ação específica (ex: criar, listar, atualizar...).
        private static void AdicionarUseCases(IServiceCollection services)
        {
            // O método AddScoped registra o serviço com tempo de vida 'scoped':
            // → A mesma instância será usada durante a requisição HTTP atual,
            // → mas uma nova instância será criada a cada nova requisição.
            //
            // Aqui estamos dizendo que toda vez que alguém pedir ICriarEquipamentoUseCase,
            // o sistema deve injetar uma instância de CriarEquipamentoUseCase.

            services.AddScoped<ICriarEquipamentoUseCase, CriarEquipamentoUseCase>();

            // Exemplo para quando você tiver mais casos de uso:
            // services.AddScoped<IAtualizarEquipamentoUseCase, AtualizarEquipamentoUseCase>();
            // services.AddScoped<IDeletarEquipamentoUseCase, DeletarEquipamentoUseCase>();
        }
    }
}
