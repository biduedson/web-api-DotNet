using Domain.Entities;

namespace Domain.Repositories.EquipamentoRepository;

/// <summary>
/// Interface responsável por definir as operações de acesso a dados relacionadas aos equipamentos.
/// </summary>
public interface IEquipamentoRepository
{
    /// <summary>
    /// Obtém um equipamento pelo seu identificador único.
    /// </summary>
    /// <param name="id">O ID do equipamento.</param>
    /// <returns>O equipamento correspondente, ou null se não encontrado.</returns>
    Task<Equipamento?> ObterPorIdAsync(Guid id);

    /// <summary>
    /// Lista todos os equipamentos cadastrados.
    /// </summary>
    /// <returns>Uma coleção de equipamentos.</returns>
    Task<IEnumerable<Equipamento>> ListarTodosAsync();

    /// <summary>
    /// Adiciona um novo equipamento ao repositório.
    /// </summary>
    /// <param name="equipamento">O equipamento a ser adicionado.</param>
    Task AdicionarEquipamentoAsync(Equipamento equipamento);

    /// <summary>
    /// Atualiza os dados de um equipamento existente.
    /// </summary>
    /// <param name="equipamento">O equipamento com os dados atualizados.</param>
    Task AtualizarAsync(Equipamento equipamento);

    /// <summary>
    /// Remove um equipamento do repositório.
    /// </summary>
    /// <param name="equipamento">O equipamento a ser removido.</param>
    Task RemoverAsync(Equipamento equipamento);
}
