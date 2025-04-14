namespace Application.DTOs.Responses.Error;

// Define uma classe que representa uma resposta de erro em formato JSON.
public class RespostasDeErro
{
    // Propriedade que armazena uma lista de mensagens de erro.
    // A interface IList<string> permite que a lista seja modificável, mas mantém flexibilidade para diferentes implementações de lista.
    public IList<string> Erros {get; set;}
    
    // Construtor que recebe uma lista de erros e a atribui à propriedade Errors.
    public RespostasDeErro(IList<string> erros) => Erros = erros;

    // Construtor que recebe uma única mensagem de erro e cria uma lista contendo esse erro.
    public RespostasDeErro(string erros)
    {   
        // Inicializa a propriedade Errors com uma nova lista contendo apenas o erro passado como argumento.
        Erros = 
        [
            erros
        ];
    }
}
