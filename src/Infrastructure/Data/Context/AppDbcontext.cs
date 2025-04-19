// Importa o namespace do Entity Framework Core, que fornece suporte a banco de dados via ORM
using Microsoft.EntityFrameworkCore;

// Importa o namespace onde está a entidade Equipamento (parte do domínio do sistema)
using Domain.Entities;

namespace Infrastructure.Data.Context
{
    /// <summary>
    /// Esta classe representa o contexto do banco de dados.
    /// Ela herda de DbContext, que é a classe base usada pelo EF Core para interagir com o banco.
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Representa uma tabela no banco chamada "Equipamentos".
        /// Cada item dessa tabela será um objeto do tipo Equipamento.
        /// </summary>
        public DbSet<Equipamento> Equipamentos { get; set; }

        /// <summary>
        /// Representa uma tabela no banco chamada "Usuarios".
        /// Cada item dessa tabela será um objeto do tipo Usuario.
        /// </summary>
        public DbSet<Usuario> Usuarios {get; set;}

        /// <summary>
        /// Representa uma tabela no banco chamada "ReservaDeEquipamento".
        /// Cada item dessa tabela será um objeto do tipo ReservaDeEquipamentos.
        /// </summary>
        public DbSet<ReservaDeEquipamento> ReservaDeEquipamentos {get; set;}

        /// <summary>
        /// Construtor que recebe as opções de configuração para o contexto do banco de dados.
        /// </summary>
        /// <param name="options">As opções de configuração que são passadas para o DbContext.</param>
        /// <remarks>
        /// O ": base(options)" significa que o construtor da classe AppDbContext
        /// está chamando o construtor da classe base, ou seja, da classe DbContext,
        /// e está passando para ele o objeto options.
        ///
        /// A classe DbContext tem um construtor que recebe DbContextOptions para configurar
        /// o comportamento do EF Core, como:
        /// - Qual banco usar (SQL Server, PostgreSQL, SQLite, etc)
        /// - String de conexão
        /// - Cache
        /// - Lazy loading
        /// - Comportamentos de log, etc
        /// 
        /// Quando fazemos ": base(options)", estamos dizendo para o DbContext
        /// usar as opções fornecidas para configurar o comportamento do banco de dados.
        /// </remarks>
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}
