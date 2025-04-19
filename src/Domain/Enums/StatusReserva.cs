namespace Domain.Enums
{
    /// <summary>
    /// O <see cref="StatusReserva"/> é um enum que representa os diferentes status
    /// que uma reserva de equipamento pode ter durante o seu ciclo de vida.
    /// </summary>
    public enum StatusReserva
    {
        /// <summary>
        /// Status inicial de uma reserva, quando ela ainda está aguardando aprovação.
        /// </summary>
        Pendente,

        /// <summary>
        /// Status que indica que a reserva foi aprovada e está pronta para ser realizada.
        /// </summary>
        Aprovada,

        /// <summary>
        /// Status que indica que a reserva foi negada por algum motivo.
        /// </summary>
        Negada,

        /// <summary>
        /// Status que indica que a reserva foi cancelada antes de ser concluída.
        /// </summary>
        Cancelada,

        /// <summary>
        /// Status que indica que a reserva foi devolvida com sucesso após o uso do equipamento.
        /// </summary>
        Devolvida
    }
}
