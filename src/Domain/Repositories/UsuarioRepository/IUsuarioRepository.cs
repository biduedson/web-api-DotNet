using Domain.Entities;

namespace Domain.Repositories.UsuarioRepository
{
    /// <summary>
    /// Interface que define o contrato para operações de persistência relacionadas à entidade <see cref="Usuario"/>.
    /// Essa interface é usada para abstrair o acesso ao banco de dados, permitindo que a aplicação utilize o repositório
    /// sem depender diretamente de uma implementação específica de persistência.
    /// </summary>
    public interface IUsuarioRepository
    {
        /// <summary>
        /// Obtém um usuário a partir do seu identificador único (ID).
        /// </summary>
        /// <param name="id">Identificador único do usuário</param>
        /// <returns>Instância de <see cref="Usuario"/> correspondente ao ID ou null se não encontrado</returns>
        Task<Usuario?> ObterPorIdAsync(Guid id);

        /// <summary>
        /// Obtém um usuário a partir de seu endereço de e-mail.
        /// </summary>
        /// <param name="email">Endereço de e-mail do usuário</param>
        /// <returns>Instância de <see cref="Usuario"/> correspondente ao e-mail ou null se não encontrado</returns>
        Task<Usuario?> ObterPorEmail(string email);

        /// <summary>
        /// Lista todos os usuários cadastrados no sistema.
        /// </summary>
        /// <returns>Lista de todos os usuários como <see cref="IEnumerable{Usuario}"/></returns>
        Task<IEnumerable<Usuario>> ListarTodosAsync();

        /// <summary>
        /// Adiciona um novo usuário ao sistema.
        /// </summary>
        /// <param name="usuario">Objeto do tipo <see cref="Usuario"/> que será persistido</param>
        Task AdicionarAsync(Usuario usuario);

        /// <summary>
        /// Atualiza os dados de um usuário existente.
        /// </summary>
        /// <param name="usuario">Objeto do tipo <see cref="Usuario"/> com os dados atualizados</param>
        Task AtualizarAsync(Usuario usuario);

        /// <summary>
        /// Verifica se um endereço de e-mail já está cadastrado no sistema.
        /// </summary>
        /// <param name="email">Endereço de e-mail a ser verificado</param>
        /// <returns><c>true</c> se o e-mail já estiver cadastrado, <c>false</c> caso contrário</returns>
        Task<bool> ExisteEmailCadastradoAsync(string email);
    }
}
