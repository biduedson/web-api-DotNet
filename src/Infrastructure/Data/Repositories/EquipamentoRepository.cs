// Importa a entidade Equipamento que será manipulada pelo repositório
using Domain.Entities;

// Importa a interface do repositório, que define o contrato que esta classe implementa
using Domain.Repositories.EquipamentoRepository;

// Importa o DbContext da aplicação, responsável por conectar com o banco de dados
using Infrastructure.Data.Context;

// Importa funcionalidades do Entity Framework Core, como métodos assíncronos para acessar o banco
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    // Implementação concreta do repositório de Equipamento, baseada na interface IEquipamentoRepository
    public class EquipamentoRepository : IEquipamentoRepository
    {
        // Injeção de dependência do contexto do banco de dados (AppDbContext)
        private readonly AppDbContext _context;

        // Construtor que recebe o contexto e armazena em um campo privado
        public EquipamentoRepository(AppDbContext context)
        {
            _context = context;
        }

        // Método responsável por adicionar um novo equipamento no banco
        public async Task AdicionarEquipamentoAsync(Equipamento equipamento)
        {
            // Adiciona a entidade de forma assíncrona no DbSet
            await _context.Equipamentos.AddAsync(equipamento);

            // Salva as alterações no banco
            await _context.SaveChangesAsync();
        }

        // Método para atualizar um equipamento já existente
        public async Task AtualizarAsync(Equipamento equipamento)
        {
            // Marca a entidade como "Modified" para que o EF Core atualize no banco
            _context.Equipamentos.Update(equipamento);

            // Aplica as mudanças no banco
            await _context.SaveChangesAsync();
        }

        // Método que busca um equipamento específico por seu ID
        public async Task<Equipamento?> ObterPorIdAsync(Guid id)
        {
            // Usa LINQ para buscar o primeiro equipamento com o ID fornecido
            // Pode retornar null se não encontrar
            return await _context.Equipamentos.FirstOrDefaultAsync(e => e.Id == id);
        }

        // Método que remove um equipamento do banco
        public async Task RemoverAsync(Equipamento equipamento)
        {
            // Marca o equipamento como "Deleted" no EF Core
            _context.Equipamentos.Remove(equipamento);

            // Aplica a remoção no banco
            await _context.SaveChangesAsync();
        }

        // Método que retorna todos os equipamentos cadastrados no banco
        public async Task<IEnumerable<Equipamento>> ListarTodosAsync()
        {
            // Retorna uma lista com todos os equipamentos (pode ser vazia)
            return await _context.Equipamentos.ToListAsync();
        }
    }
}
