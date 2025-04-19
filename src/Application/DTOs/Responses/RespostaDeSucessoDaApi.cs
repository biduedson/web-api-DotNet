namespace Application.DTOs.Responses
{
    /// <summary>
    /// Representa uma resposta padrão de sucesso da API.
    /// </summary>
    /// <typeparam name="T">Tipo genérico dos dados retornados pela API.</typeparam>
    /// <remarks>
    /// Essa classe é utilizada para estruturar a resposta de sucesso de uma requisição, incluindo uma flag de sucesso,
    /// uma mensagem descritiva e os dados retornados.
    /// </remarks>
    public class RespostaDeSucessoDaApi<T>
    {
        /// <summary>
        /// Indica se a operação foi bem-sucedida.
        /// </summary>
        /// <remarks>
        /// Essa propriedade é comumente usada para validar se a requisição executou com sucesso. O valor padrão é <c>false</c>,
        /// a menos que seja atribuído como <c>true</c> explicitamente.
        /// </remarks>
        public bool Succes { get; set; } 

        /// <summary>
        /// Mensagem informativa sobre o resultado da operação.
        /// </summary>
        /// <remarks>
        /// Pode ser usada para descrever a ação realizada ou fornecer feedback adicional ao cliente. É inicializada como uma string vazia.
        /// </remarks>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Dados retornados pela API em caso de sucesso.
        /// </summary>
        /// <remarks>
        /// A propriedade é genérica e pode conter qualquer tipo de dado relacionado à operação executada.
        /// Pode ser <c>null</c> caso não haja dados específicos a serem retornados.
        /// </remarks>
        public T? Data { get; set; }
    }
}
