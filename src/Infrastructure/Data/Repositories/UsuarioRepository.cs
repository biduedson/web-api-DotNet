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
    // Implementação concreta do repositório de Usuario, baseada na interface IUsuarioRepository
    public class UsuarioRepository : IUsuarioRepository
    {
        // Injeção de dependência do contexto do banco de dados (AppDbContext)
        private readonly AppDbContext _context;

        // Construtor que recebe o contexto e armazena em um campo privado
        public UsuarioRepository(AppDbContext context) => _context = context;
       
        // Método que busca um equipamento específico por seu ID
        public async Task<Usuario?> ObterPorIdAsync(Guid id)
        {
          // Usa LINQ para buscar o primeiro usuario com o ID fornecido
          // Pode retornar null se não encontrar
          return await _context.Usuarios.FirstOrDefaultAsync(usuario => usuario.Id == id);
        }

        // Método que retorna todos os equipamentos cadastrados no banco
        public async Task<IEnumerable<Usuario>> ListarTodosAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

        // Método responsável por adicionar um novo Usuario no banco
        public async Task AdicionarAsync(Usuario usuario)
        {
            // Adiciona a entidade de forma assíncrona no DbSet
            await _context.Usuarios.AddAsync(usuario);

             // Salva as alterações no banco
            await _context.SaveChangesAsync();
        }

        // Método para atualizar um Usuario já existente
        public async Task AtualizarAsync(Usuario usuario)
        {
          // Marca a entidade como "Modified" para que o EF Core atualize no banco
          _context.Usuarios.Update(usuario);

          // Salva as alterações no banco
          await _context.SaveChangesAsync();
        }
        
        // Método assíncrono que verifica se já existe um usuário ativo com o e-mail informado
        public async Task<bool> ExisteEmailCadastradoAsync(string email) =>
          await _context.Usuarios.AnyAsync(usuario => usuario.Email.Equals(email) && usuario.Ativo);
        
    }
}