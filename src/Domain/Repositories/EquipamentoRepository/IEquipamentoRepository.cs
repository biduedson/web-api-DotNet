using Domain.Entities;

namespace Domain.Repositories.EquipamentoRepository;

public interface IEquipamentoRepository
{
    Task<Equipamento?> ObterPorIdAsync(Guid id);
    Task<IEnumerable<Equipamento>> ListarTodosAsync();
    Task AdicionarEquipamentoAsync(Equipamento equipamento);
    Task AtualizarAsync(Equipamento equipamento);
    Task RemoverAsync(Equipamento equipamento);
}