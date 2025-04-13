public class RespostaDeSucessoDaApi<T>
{
    public bool Succes {get; set;} 
    public string Message {get; set;} = string.Empty;
    public T? Data {get; set;}
}