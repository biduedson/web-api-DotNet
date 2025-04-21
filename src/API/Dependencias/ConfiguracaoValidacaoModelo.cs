using Microsoft.AspNetCore.Mvc;

namespace API.Dependencias
{
    public static class ConfiguracaoValidacaoModelo
    {
        /// <summary>
        /// Configura o comportamento padrão de validação de modelo no ASP.NET Core
        /// Desabilita o retorno automático de BadRequest quando o ModelState é inválido,
        /// pois a aplicação já possui um filtro de exceções personalizado para esse tipo de validação.
        /// </summary>
        /// <param name="services">Coleção de serviços onde a configuração será aplicada</param>
        public static void ConfigurarValidacaoPersonalizada(IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
        }
    }
}