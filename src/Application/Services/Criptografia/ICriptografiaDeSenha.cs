namespace Application.Services.Criptografia
{
    /// <summary>
    /// Interface responsável por definir a operação de criptografia de senha.
    /// </summary>
    public interface ICriptografiaDeSenha
    {
        /// <summary>
        /// Método responsável por criptografar a senha fornecida.
        /// </summary>
        /// <param name="senha">Senha em texto simples a ser criptografada.</param>
        /// <returns>Senha criptografada.</returns>
        public string CriptografarSenha(string senha);
    }
}
