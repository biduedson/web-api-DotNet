

using Domain.Entities;

namespace Domain.Repositories.UsuarioRepository;

public interface IUsuarioRepository
{
    Task<Usuario?> ObterPorIdAsync(Guid id);
    Task<IEnumerable<Usuario>> ListarTodosAsync();
    Task AdicionarAsync(Usuario usuario);
    Task AtualizarAsync(Usuario usuario);
    Task<bool> ExisteEmailCadastradoAsync(string email);
}