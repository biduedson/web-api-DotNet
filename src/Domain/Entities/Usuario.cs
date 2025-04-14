namespace Domain.Entities;

public class Usuario 
{
    public Guid Id {get; set; }
    public string Nome {get;set;} = string.Empty;
    public string Email {get; set;} = string.Empty;
    public string Senha {get; set;} = string.Empty; 
    public bool Ativo { get; set; } = true;
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    public bool Administrador {get; set;} = false;


    public void TornarUsuarioAdmin() => Administrador = true;
}