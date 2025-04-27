using System.Text.Json.Serialization;

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
        [JsonPropertyName("nome")]
        public string Nome { get; set; } = string.Empty;

        /// <summary>
        /// Endereço de e-mail do usuário a ser registrado.
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Senha do usuário para autenticação.
        /// </summary>
        [JsonPropertyName("senha")]
        public string Senha { get; set; } = string.Empty;
    }
}
