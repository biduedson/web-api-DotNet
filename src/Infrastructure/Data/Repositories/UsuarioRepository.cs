// Importa a entidade Usuario que será manipulada pelo repositório
using Domain.Entities;

// Importa a interface do repositório, que define o contrato que esta classe implementa
using Domain.Repositories.UsuarioRepository;

// Importa o DbContext da aplicação, responsável por conectar com o banco de dados
using Infrastructure.Data.Context;

// Importa funcionalidades do Entity Framework Core, como métodos assíncronos para acessar o banco
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    /// <summary>
    /// Implementação concreta do repositório de Usuario, baseada na interface IUsuarioRepository.
    /// Essa classe é responsável por acessar o banco de dados para operações relacionadas à entidade Usuario.
    /// </summary>
    public class UsuarioRepository : IUsuarioRepository
    {
        // Injeção de dependência do contexto do banco de dados (AppDbContext)
        private readonly AppDbContext _context;

        /// <summary>
        /// Construtor que recebe o contexto e o armazena em um campo privado.
        /// </summary>
        public UsuarioRepository(AppDbContext context) => _context = context;

        /// <summary>
        /// Busca um usuário específico pelo seu identificador único (ID).
        /// </summary>
        /// <param name="id">Identificador único do usuário.</param>
        /// <returns>Usuário correspondente ao ID ou null se não encontrado.</returns>
        public async Task<Usuario?> ObterPorIdAsync(Guid id)
        {
            // Usa LINQ para buscar o primeiro usuário com o ID fornecido
            return await _context.Usuarios.FirstOrDefaultAsync(usuario => usuario.Id == id);
        }

        /// <summary>
        /// Busca um usuário específico pelo seu e-mail.
        /// </summary>
        /// <param name="email">E-mail do usuário.</param>
        /// <returns>Usuário correspondente ao e-mail ou null se não encontrado.</returns>
        public async Task<Usuario?> ObterPorEmail(string email)
        {
            // Usa LINQ para buscar o primeiro usuário com o e-mail fornecido
            return await _context.Usuarios.FirstOrDefaultAsync(usuario => usuario.Email == email);
        }

        /// <summary>
        /// Retorna todos os usuários cadastrados no banco de dados.
        /// </summary>
        /// <returns>Lista de todos os usuários.</returns>
        public async Task<IEnumerable<Usuario>> ListarTodosAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

        /// <summary>
        /// Adiciona um novo usuário no banco de dados.
        /// </summary>
        /// <param name="usuario">Objeto do tipo Usuario que será adicionado.</param>
        public async Task AdicionarAsync(Usuario usuario)
        {
            // Adiciona a entidade de forma assíncrona no DbSet
            await _context.Usuarios.AddAsync(usuario);

            // Salva as alterações no banco
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Atualiza os dados de um usuário existente no banco de dados.
        /// </summary>
        /// <param name="usuario">Usuário com os dados atualizados.</param>
        public async Task AtualizarAsync(Usuario usuario)
        {
            // Marca a entidade como "Modified" para que o EF Core a atualize no banco
            _context.Usuarios.Update(usuario);

            // Salva as alterações no banco
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Verifica se já existe um usuário ativo com o e-mail informado.
        /// </summary>
        /// <param name="email">E-mail que será verificado.</param>
        /// <returns>True se o e-mail já estiver cadastrado e ativo, false caso contrário.</returns>
        public async Task<bool> ExisteEmailCadastradoAsync(string email) =>
            await _context.Usuarios.AnyAsync(usuario => usuario.Email.Equals(email) && usuario.Ativo);
    }
}
