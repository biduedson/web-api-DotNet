
namespace Application.DTOs.Requests;
public class RegistrarUsuarioRequest
{
    public string Nome {get;set;} = string.Empty;
    public string Email {get; set;} = string.Empty;
    public string Senha {get; set;} = string.Empty; 
}