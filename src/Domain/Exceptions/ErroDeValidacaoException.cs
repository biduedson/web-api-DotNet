namespace Domain.Exceptions
{
    /// <summary>
    /// A <see cref="ErroDeValidacaoException"/> é uma exceção personalizada que herda de <see cref="DomainException"/>.
    /// Ela é usada para representar erros de validação durante o processo de execução da aplicação.
    /// Esses erros podem ocorrer quando um dado ou condição não atende a uma regra de negócio ou critério pré-estabelecido.
    /// </summary>
    public class ErroDeValidacaoException : DomainException
    {
        // Lista que contém todas as mensagens de erro de validação.
        public IList<string> MenssagensDeErro { get; set; }

        /// <summary>
        /// Construtor para inicializar a exceção de validação com uma lista de mensagens de erro.
        /// </summary>
        /// <param name="menssagensDeErro">Lista de mensagens que descrevem as falhas de validação.</param>
        public ErroDeValidacaoException(IList<string> menssagensDeErro)
        {
            MenssagensDeErro = menssagensDeErro;
        }
    }
}
