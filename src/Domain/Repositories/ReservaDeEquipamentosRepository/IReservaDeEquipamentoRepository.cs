using Domain.Entities;
using Domain.Enums;

namespace Domain.Repositories.ReservaDeEquipamentosRepository;

/// <summary>
/// Interface responsável por definir as operações de acesso a dados relacionadas às reservas de equipamentos.
/// </summary>
public interface IReservaDeEquipamentosRepository
{
    /// <summary>
    /// Adiciona uma nova reserva de equipamento ao repositório.
    /// </summary>
    /// <param name="reservaDeEquipamento">A reserva a ser adicionada.</param>
    /// <returns>A reserva adicionada, incluindo informações atualizadas do banco de dados.</returns>
    Task<ReservaDeEquipamento> AdicionarReserva(ReservaDeEquipamento reservaDeEquipamento);

    /// <summary>
    /// Obtém uma reserva específica pelo seu identificador.
    /// </summary>
    /// <param name="id">O ID da reserva.</param>
    /// <returns>A reserva correspondente, ou null se não encontrada.</returns>
    Task<ReservaDeEquipamento?> ObterReservaPorId(Guid id);

    /// <summary>
    /// Lista todas as reservas de equipamentos cadastradas.
    /// </summary>
    /// <returns>Uma coleção de reservas.</returns>
    Task<IEnumerable<ReservaDeEquipamento>> ListarTodasAsReservas();

    /// <summary>
    /// Altera o status de uma reserva existente.
    /// </summary>
    /// <param name="reserva">A reserva que terá o status alterado.</param>
    /// <param name="status">O novo status da reserva.</param>
    /// <returns>A reserva atualizada.</returns>
    Task<ReservaDeEquipamento> AlterarStatusDaReserva(ReservaDeEquipamento reserva, StatusReserva status);

    /// <summary>
    /// Altera a data de uma reserva existente.
    /// </summary>
    /// <param name="reserva">A reserva que terá a data alterada.</param>
    /// <param name="novaData">A nova data da reserva.</param>
    /// <returns>A reserva atualizada.</returns>
    Task<ReservaDeEquipamento> AlterarDataDaReserva(ReservaDeEquipamento reserva, DateTime novaData);

    /// <summary>
    /// Exclui uma reserva de equipamento do repositório.
    /// </summary>
    /// <param name="reserva">A reserva a ser excluída.</param>
    Task ExcluirReserva(ReservaDeEquipamento reserva);
}
