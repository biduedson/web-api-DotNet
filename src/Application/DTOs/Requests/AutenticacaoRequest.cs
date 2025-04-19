namespace Application.DTOs.Requests
{
    /// <summary>
    /// Classe de solicitação de autenticação contendo as credenciais do usuário.
    /// </summary>
    public class AutenticacaoRequest
    {
        /// <summary>
        /// E-mail do usuário para autenticação.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Senha do usuário para autenticação.
        /// </summary>
        public string Senha { get; set; } = string.Empty;
    }
}
