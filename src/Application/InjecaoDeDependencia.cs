// Importações necessárias para o uso do AutoMapper, configuração e injeção de dependência
using Application.UseCases.CriarEquipamento;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Application.UseCases.CriarUsuario;
using Application.UseCases.Autenticacao;
using Application.UseCases.ReservaDeEquipamentoUseCase;

namespace Application
{
    /// <summary>
    /// Classe estática usada para registrar todos os serviços da camada de aplicação.
    /// É comum criarmos uma classe com esse nome (InjecaoDeDependencia ou DependencyInjection)
    /// em cada camada (Application, Infra, WebAPI, etc.) para manter as responsabilidades separadas.
    /// </summary>
    public static class InjecaoDeDependencia
    {
        /// <summary>
        /// Método de extensão que será chamado a partir do Program.cs.
        /// Ele recebe dois parâmetros:
        /// - <see cref="IServiceCollection"/> services → é a lista de todos os serviços que sua aplicação conhece.
        /// - <see cref="IConfiguration"/> configuration → representa o arquivo appsettings.json, variáveis de ambiente e outras configs.
        /// </summary>
        /// <param name="services">Lista de serviços que a aplicação conhece.</param>
        /// <param name="configuration">Configurações do arquivo appsettings.json.</param>
        public static void AdicionarApplication(this IServiceCollection services, IConfiguration configuration)
        {

            // Registra os perfis do AutoMapper da camada Application
            AdicionarAutoMapper(services);

            // Registra os casos de uso da aplicação
            AdicionarUseCases(services);
        }

        /// <summary>
        /// Este método adiciona o AutoMapper à injeção de dependência.
        /// O AutoMapper é uma biblioteca que facilita o mapeamento entre objetos (por exemplo, DTO → Entidade).
        /// </summary>
        /// <param name="services">Lista de serviços para registrar o AutoMapper.</param>
        private static void AdicionarAutoMapper(IServiceCollection services)
        {
            // Registra automaticamente todos os perfis de mapeamento (classes que herdam de Profile)
            // localizados no mesmo assembly da classe InjecaoDeDependencia.
            services.AddAutoMapper(typeof(InjecaoDeDependencia).Assembly);
        }

        /// <summary>
        /// Aqui registramos os casos de uso (use cases), que são as regras de negócio da aplicação.
        /// Cada use case é uma classe que executa uma ação específica (ex: criar, listar, atualizar...).
        /// </summary>
        /// <param name="services">Lista de serviços onde os casos de uso serão registrados.</param>
        private static void AdicionarUseCases(IServiceCollection services)
        {
            /// <summary>
            /// O que significa AddScoped?
            /// - Significa que a instância da classe será criada **uma única vez por requisição HTTP**.
            /// - Durante a mesma requisição, todos os lugares que precisarem do serviço compartilharão a mesma instância.
            /// - Em uma nova requisição, será criada uma nova instância.
            ///
            /// Esse comportamento é ideal para serviços que utilizam DbContext, por exemplo, onde a conexão/transação
            /// precisa ser mantida durante toda a requisição.
            /// </summary>

            services.AddScoped<ICriarEquipamentoUseCase, CriarEquipamentoUseCase>();
            services.AddScoped<ICriarUsuarioUseCase, CriarUsuarioUseCase>();
            services.AddScoped<IAutenticacaoUseCase, AutenticacaoUseCase>();
            services.AddScoped<ICriarReservaDeEquipamentoUseCase, CriarReservaDeEquipamentoUseCase>();
            // Adicione outros casos de uso aqui conforme necessário:
            // services.AddScoped<IAtualizarEquipamentoUseCase, AtualizarEquipamentoUseCase>();
            // services.AddScoped<IDeletarUsuarioUseCase, DeletarUsuarioUseCase>();
        }
    }
}
