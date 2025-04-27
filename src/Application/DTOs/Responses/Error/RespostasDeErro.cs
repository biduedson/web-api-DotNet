namespace Application.DTOs.Responses.Error
{
    /// <summary>
    /// Representa uma resposta de erro padronizada para a API.
    /// </summary>
    /// <remarks>
    /// Essa classe é utilizada para encapsular mensagens de erro que podem ser retornadas para o cliente em casos de falhas de validação,
    /// exceções tratadas, ou qualquer outra situação que demande a notificação de um ou mais erros.
    /// </remarks>
    public class RespostasDeErro
    {
        /// <summary>
        /// Lista contendo as mensagens de erro.
        /// </summary>
        /// <remarks>
        /// A propriedade é do tipo <see cref="IList{T}"/> com <see cref="string"/> como tipo genérico, permitindo múltiplas mensagens de erro.
        /// A interface <see cref="IList{T}"/> oferece flexibilidade quanto à implementação, mantendo a possibilidade de modificação da lista.
        /// </remarks>
        public IList<string> Erros { get; set; }

        /// <summary>
        /// Construtor que recebe uma lista de mensagens de erro.
        /// </summary>
        /// <param name="erros">Lista de strings representando mensagens de erro.</param>
        /// <remarks>
        /// Esse construtor é ideal para quando já se tem múltiplas mensagens de erro a serem retornadas.
        /// </remarks>
        public RespostasDeErro(IList<string> erros)
        {
            Erros = erros;
        }

        /// <summary>
        /// Construtor que recebe uma única mensagem de erro.
        /// </summary>
        /// <param name="erros">Mensagem de erro única como string.</param>
        /// <remarks>
        /// Esse construtor facilita o retorno de erros simples ao encapsular automaticamente a mensagem em uma lista.
        /// </remarks>
        public RespostasDeErro(string erro)
        {
            Erros =
            [
                erro
            ];
        }
    }
}
