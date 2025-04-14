namespace Domain.Entities;

public class Usuario 
{
    public Guid Id {get; set; }
    public string Nome {get;set;} = string.Empty;
    public string Email {get; set;} = string.Empty;
    public string Senha {get; set;} = string.Empty;
    public bool IsAdmin {get; set;} = false;


    public void TornarUsuarioAdmin() => IsAdmin = true;
}