namespace Application.DTOs.Requests
{
    /// <summary>
    /// Classe de solicitação para registrar um novo usuário.
    /// </summary>
    public class RegistrarUsuarioRequest
    {
        /// <summary>
        /// Nome completo do usuário a ser registrado.
        /// </summary>
        public string Nome { get; set; } = string.Empty;

        /// <summary>
        /// Endereço de e-mail do usuário a ser registrado.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Senha do usuário para autenticação.
        /// </summary>
        public string Senha { get; set; } = string.Empty;
    }
}
