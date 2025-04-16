// Importações necessárias para o uso do AutoMapper, configuração e injeção de dependência
using Application.UseCases.CriarEquipamento;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Application.UseCases.CriarUsuario;
using Application.criptografia;
using Application.UseCases.autenticacao;
using Application.UseCases.Autenticacao;

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

        public static void AdicionarApplication(this IServiceCollection services, IConfiguration configuration)
        {

            // Adiciona o serviço de criptografia de senha.
            AdicionarCriptografiaDesenha(services,configuration);

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

        // Método privado para adicionar o serviço de criptografia de senha no contêiner de dependência.
        // A chave adicional de criptografia é obtida da configuração da aplicação.

        private static void AdicionarCriptografiaDesenha(IServiceCollection services, IConfiguration configuration)
        {
            // Obtém a chave adicional de criptografia das configurações.
            var AdditionalKey = configuration.GetValue<string>("Settings:Passwords:AdditionalKey");
            services.AddScoped(option => new CriptografiaDeSenha(AdditionalKey!));
        }

        // Aqui registramos os casos de uso (use cases), que são as regras de negócio da aplicação.
        // Cada use case é uma classe que executa uma ação específica (ex: criar, listar, atualizar...).
        private static void AdicionarUseCases(IServiceCollection services)
        {
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
            
            services.AddScoped<ICriarEquipamentoUseCase, CriarEquipamentoUseCase>();
            services.AddScoped<ICriarUsuarioUseCase, CriarUsuarioUseCase>();
            services.AddScoped<IAutenticacaoUseCase, AutenticacaoUseCase>();
            // Adicione outros casos de uso aqui conforme necessário:
            // services.AddScoped<IAtualizarEquipamentoUseCase, AtualizarEquipamentoUseCase>();
            // services.AddScoped<IDeletarUsuarioUseCase, DeletarUsuarioUseCase>();
        }
    }
}
