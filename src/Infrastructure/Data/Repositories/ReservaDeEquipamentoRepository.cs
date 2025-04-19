using Domain.Entities;
using Domain.Enums;
using Domain.Repositories.ReservaDeEquipamentosRepository;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    /// <summary>
    /// Implementação concreta do repositório de reservas de equipamento.
    /// Responsável por realizar operações de persistência com a entidade ReservaDeEquipamento.
    /// </summary>
    public class ReservaDeEquipamentoRepository : IReservaDeEquipamentosRepository
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Injeta o contexto da aplicação para acesso ao banco de dados.
        /// </summary>
        /// <param name="context">Instância do AppDbContext.</param>
        public ReservaDeEquipamentoRepository(AppDbContext context) => _context = context;

        /// <summary>
        /// Adiciona uma nova reserva de equipamento ao banco de dados.
        /// </summary>
        /// <param name="reservaDeEquipamento">Reserva a ser adicionada.</param>
        /// <returns>A reserva adicionada, incluindo o ID gerado.</returns>
        public async Task<ReservaDeEquipamento> AdicionarReserva(ReservaDeEquipamento reservaDeEquipamento)
        {
            await _context.ReservaDeEquipamentos.AddAsync(reservaDeEquipamento);
            await _context.SaveChangesAsync();
            return   reservaDeEquipamento;
        }

        /// <summary>
        /// Busca uma reserva de equipamento pelo seu ID.
        /// </summary>
        /// <param name="id">ID da reserva.</param>
        /// <returns>Reserva encontrada ou null se não existir.</returns>
        public async Task<ReservaDeEquipamento?> ObterReservaPorId(Guid id)
        {
            return await _context.ReservaDeEquipamentos
                .FirstOrDefaultAsync(reserva => reserva.Id == id);
        }

        /// <summary>
        /// Lista todas as reservas de equipamento cadastradas.
        /// </summary>
        /// <returns>Lista com todas as reservas.</returns>
        public async Task<IEnumerable<ReservaDeEquipamento>> ListarTodasAsReservas()
        {
            return await _context.ReservaDeEquipamentos.ToListAsync();
        }

        /// <summary>
        /// Altera o status de uma reserva de equipamento.
        /// </summary>
        /// <param name="reserva">Reserva que terá o status alterado.</param>
        /// <param name="status">Novo status da reserva.</param>
        /// <returns>Reserva atualizada com o novo status.</returns>
        public async Task<ReservaDeEquipamento> AlterarStatusDaReserva(ReservaDeEquipamento reserva, StatusReserva status)
        {
            reserva.Status = status;
            _context.ReservaDeEquipamentos.Update(reserva);
            await _context.SaveChangesAsync();
            return reserva;
        }

        /// <summary>
        /// Altera a data de uma reserva de equipamento.
        /// </summary>
        /// <param name="reserva">Reserva que terá a data alterada.</param>
        /// <param name="novaData">Nova data da reserva.</param>
        /// <returns>Reserva atualizada com a nova data.</returns>
        public async Task<ReservaDeEquipamento> AlterarDataDaReserva(ReservaDeEquipamento reserva, DateTime novaData)
        {
            reserva.DataDaReserva = novaData;
            _context.ReservaDeEquipamentos.Update(reserva);
            await _context.SaveChangesAsync();
            return reserva;
        }

        /// <summary>
        /// Exclui uma reserva de equipamento do banco de dados.
        /// </summary>
        /// <param name="reserva">Reserva a ser excluída.</param>
        public async Task ExcluirReserva(ReservaDeEquipamento reserva)
        {
            _context.ReservaDeEquipamentos.Remove(reserva);
            await _context.SaveChangesAsync();
        }
    }
}
