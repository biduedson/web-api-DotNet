namespace Domain.Entities;

public class Equipamento
{
    public Guid Id {get; set;}
    public string Nome {get; set;} = string.Empty;
    public string Descricao {get; set;} = string.Empty;
    public List<ReservaDeEquipamento> Reservas {get; set;} = new();
    
    public void AtualizarDescricao(string descricao){
        Descricao = descricao;
    }
    public void AtualizarNome(string nome){
        Nome = nome;
    }
}