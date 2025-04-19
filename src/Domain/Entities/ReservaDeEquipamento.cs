using Domain.Enums;

namespace Domain.Entities
{
    /// <summary>
    /// A classe <see cref="ReservaDeEquipamento"/> representa uma entidade no domínio da aplicação que
    /// mapeia uma reserva feita por um usuário para um equipamento específico.
    /// 
    /// <para>
    /// Entidades possuem identidade única e podem estar associadas a outras entidades. A <see cref="ReservaDeEquipamento"/>
    /// tem um identificador único <see cref="Id"/>, relaciona-se com outras entidades como o <see cref="Usuario"/>
    /// e o <see cref="Equipamento"/>, e possui um status de reserva indicado por <see cref="Status"/>.
    /// </para>
    /// </summary>
    public class ReservaDeEquipamento
    {
        /// <summary>
        /// Identificador único da reserva de equipamento.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Identificador do usuário que fez a reserva.
        /// </summary>
        public Guid UsuarioId { get; set; }

        /// <summary>
        /// Identificador do equipamento que foi reservado.
        /// </summary>
        public Guid EquipamentoId { get; set; }

        /// <summary>
        /// Data em que a reserva foi feita.
        /// </summary>
        public DateTime DataDaReserva { get; set; }

        /// <summary>
        /// Status atual da reserva, que pode ser "Pendente", "Confirmada", "Cancelada", etc.
        /// </summary>
        public StatusReserva Status { get; set; }

        /// <summary>
        /// O usuário que fez a reserva. Relacionamento com a entidade <see cref="Usuario"/>.
        /// </summary>
        public Usuario Usuario { get; set; } = null!;

        /// <summary>
        /// O equipamento que foi reservado. Relacionamento com a entidade <see cref="Equipamento"/>.
        /// </summary>
        public Equipamento Equipamento { get; set; } = null!;
    }
}
