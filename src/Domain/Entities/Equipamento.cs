namespace Domain.Entities;

public class Equipamento
{
    public Guid Id {get; set;}
    public string Nome {get; set;} = string.Empty;
    public string Descricao {get; set;} = string.Empty;

    public Equipamento(string nome, string descricao){
        Id = Guid.NewGuid();
        Nome = nome;
        Descricao = descricao;
    }

    public void AtualizarDescricao(string descricao){
        Descricao = descricao;
    }
}