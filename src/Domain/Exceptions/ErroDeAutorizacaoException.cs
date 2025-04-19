namespace Domain.Exceptions
{
    /// <summary>
    /// A <see cref="ErroDeAutorizacaoException"/> é uma exceção personalizada que herda de <see cref="DomainException"/>.
    /// Ela é usada para representar erros específicos de autorização, como quando um usuário tenta acessar um recurso sem ter permissão.
    /// </summary>
    public class ErroDeAutorizacaoException : DomainException
    {
        // Propriedade que contém a mensagem de erro relacionada à falha de autorização.
        public string MenssagenDeErro { get; set; }

        /// <summary>
        /// Construtor para inicializar a exceção de autorização com uma mensagem de erro específica.
        /// </summary>
        /// <param name="menssagenDeErro">Mensagem de erro relacionada à falha de autorização.</param>
        public ErroDeAutorizacaoException(string menssagenDeErro)
        {
            MenssagenDeErro = menssagenDeErro;
        }
    }
}
