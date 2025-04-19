namespace Domain.Exceptions
{
    /// <summary>
    /// A <see cref="ErroDeAutenticacaoException"/> é uma exceção personalizada que herda de <see cref="DomainException"/>.
    /// Ela é usada para representar erros específicos de autenticação, como falhas ao tentar autenticar um usuário.
    /// </summary>
    public class ErroDeAutenticacaoException : DomainException
    {
        // Propriedade que contém a mensagem de erro relacionada à falha de autenticação.
        public string MenssagenDeErro { get; set; }

        /// <summary>
        /// Construtor para inicializar a exceção de autenticação com uma mensagem de erro específica.
        /// </summary>
        /// <param name="mensagemDeErro">Mensagem de erro relacionada à falha de autenticação.</param>
        public ErroDeAutenticacaoException(string mensagemDeErro)
        {
            MenssagenDeErro = mensagemDeErro;
        }
    }
}
