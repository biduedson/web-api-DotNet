using Domain.Enums;

namespace Application.DTOs.Requests
{
    /// <summary>
    /// Classe de solicitação para realizar a reserva de um equipamento.
    /// </summary>
    public class ReservaDeEquipamentoRequest
    {
        /// <summary>
        /// Identificador único do usuário que está realizando a reserva.
        /// </summary>
        public Guid UsuarioId { get; set; }

        /// <summary>
        /// Identificador único do equipamento a ser reservado.
        /// </summary>
        public Guid EquipamentoId { get; set; }

        /// <summary>
        /// Data e hora em que o equipamento será reservado.
        /// </summary>
        public DateTime DataDaReserva { get; set; }

        /// <summary>
        /// Status atual da reserva do equipamento.
        /// </summary>
        public string Status { get; set; }  = string.Empty;
    }
}
