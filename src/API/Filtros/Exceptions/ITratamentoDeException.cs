using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filtros.Exceptions
{
    /// <summary>
    /// Interface para estratégias de tratamento de exceções
    /// </summary> 
    public interface ITratamentoDeEcxeption
    {
        /// <summary>
        /// Verifica se o tratador pode processar a exceção
        /// </summary>
        bool PodeTratar(Exception exception);

        /// <summary>
        /// Trata a exceção específica
        /// </summary>
        void Tratar(ExceptionContext context);
    }
}