using Domain.Enums;

namespace Domain.Entities;

public class ReservaDeEquipamento
{
    public Guid Id {get; set;}
    public Guid UsuarioId {get; set;}
    public Guid EquipamentoId {get; set;}
    public DateTime DataDaReserva {get; set;} 
    public StatusReserva Status {get; set;}
    public Usuario Usuario {get; set;} = null!;
    public Equipamento Equipamento {get; set;} = null!;
}