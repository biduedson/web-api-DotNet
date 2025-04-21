using API.Dependencias;
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
            ConfigurarSwagger.AdicionarSwagger(services);

            // Configuração de CORS
            ConfiguracaoDeCors.ConfigurarCors(services);

            // Configuração de ModelState
            ConfiguracaoValidacaoModelo.ConfigurarValidacaoPersonalizada(services);

        }

    }
}