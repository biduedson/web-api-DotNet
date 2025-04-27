namespace Application.Services.Token
{
    /// <summary>
    /// Interface responsável pelos serviços relacionados ao gerenciamento de tokens JWT.
    /// </summary>
    public interface IServicoDeToken
    {
        /// <summary>
        /// Método responsável por gerar um token JWT para um usuário.
        /// </summary>
        /// <param name="idUsuario">ID do usuário para o qual o token será gerado.</param>
        /// <param name="email">Email do usuário.</param>
        /// <param name="administrador">Flag que indica se o usuário é um administrador.</param>
        /// <returns>Token JWT gerado.</returns>
        string GerarToken(Guid idUsuario, string email, bool administrador);

        Guid? ObterIdDoUsuarioDoToken(string token);

        /// <summary>
        /// Método responsável por obter a data de expiração de um token JWT.
        /// </summary>
        /// <param name="token">Token JWT.</param>
        /// <returns>A data de expiração do token.</returns>
        DateTime ObterDataExpiracaoToken(string token);

        /// <summary>
        /// Método responsável por obter o tipo de usuario (se e usuario comum ou admistrador.).
        /// </summary>
        /// <param name="token">Token JWT.</param>
        /// <returns>A data de expiração do token.</returns>
        string ObterTipoDeUsuario(string token);
    }
}
