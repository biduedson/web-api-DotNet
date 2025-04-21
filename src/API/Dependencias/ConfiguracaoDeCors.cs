namespace API.Dependencias
{
    /// <summary>
    /// Classe responsável pela configuração de políticas CORS (Cross-Origin Resource Sharing) na aplicação.
    /// CORS é um mecanismo que permite que recursos restritos em uma página web sejam solicitados
    /// por outro domínio fora do domínio do qual o recurso se originou.
    /// </summary>
    public static class ConfiguracaoDeCors
    {
        /// <summary>
        /// Configura a política de CORS para a aplicação.
        /// Esta implementação permite solicitações de qualquer origem (AllowAnyOrigin),
        /// com qualquer método HTTP (GET, POST, PUT, DELETE, etc.) e com qualquer cabeçalho HTTP.
        /// Isso é útil para ambientes de desenvolvimento ou APIs públicas, mas em ambientes
        /// de produção pode ser mais seguro restringir origens, métodos e cabeçalhos específicos.
        /// </summary>
        /// <param name="services">Coleção de serviços onde a configuração CORS será adicionada</param>
        public static void ConfigurarCors(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("PoliticaCORS", builder =>
                {
                    builder
                    .AllowAnyOrigin()    // Permite requisições de qualquer origem
                    .AllowAnyMethod()    // Permite qualquer método HTTP (GET, POST, PUT, DELETE, etc.)
                    .AllowAnyHeader();   // Permite qualquer cabeçalho na requisição
                });
            });
        }
    }
}