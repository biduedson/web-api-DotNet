// Importa o namespace do Entity Framework Core, que fornece suporte a banco de dados via ORM
using Microsoft.EntityFrameworkCore;

// Importa o namespace onde está a entidade Equipamento (parte do domínio do sistema)
using Domain.Entities;

namespace Infrastructure.Data.Context
{
    // Esta classe representa o contexto do banco de dados.
    // Ela herda de DbContext, que é a classe base usada pelo EF Core para interagir com o banco.
    public class AppDbContext : DbContext
    {
        // Representa uma tabela no banco chamada "Equipamentos".
        // Cada item dessa tabela será um objeto do tipo Equipamento.
        public DbSet<Equipamento> Equipamentos { get; set; }

        // Representa uma tabela no banco chamada "Usuarios".
        // Cada item dessa tabela será um objeto do tipo Usuario.
        public DbSet<Usuario> Usuarios {get; set;}

        // Representa uma tabela no banco chamada "ReservaDeEquipamento".
        // Cada item dessa tabela será um objeto do tipo ReservaDeEquipamentos.
        public DbSet<ReservaDeEquipamento> ReservaDeEquipamentos {get; set;}

        // ============================================================
        // O que significa ": base(options)"?
        //
        // O ": base(options)" significa que o construtor da classe AppDbContext
        // está chamando o construtor da classe base, ou seja, da classe DbContext,
        // e está passando para ele o objeto options.
        //
        // ✅ Por quê?
        // A classe AppDbContext herda de DbContext, assim:
        //
        //     public class AppDbContext : DbContext
        //
        // A classe DbContext tem um construtor que recebe DbContextOptions para configurar
        // o comportamento do EF Core, como:
        //
        // - Qual banco usar (SQL Server, PostgreSQL, SQLite, etc)
        // - String de conexão
        // - Cache
        // - Lazy loading
        // - Comportamentos de log, etc
        //
        // Então quando fazemos:
        //
        //     : base(options)
        //
        // Estamos dizendo:
        // “Quando alguém criar um AppDbContext, use essas opções para configurar
        // a classe DbContext que está por baixo dos panos.”
        //
        // 🧠 Analogia simples:
        // Imagine que AppDbContext é um carro, e DbContext é o motor.
        //
        // Você está criando um carro com um motor e passando a configuração do motor assim:
        //
        //     public Carro(MotorConfig config) : base(config) {}
        //
        // O base(config) entrega a configuração pro motor.
        //
        // 🧪 Exemplo real:
        // No Program.cs ou Startup.cs, você verá algo assim:
        //
        //     builder.Services.AddDbContext<AppDbContext>(options =>
        //         options.UseSqlServer("sua-connection-string-aqui"));
        //
        // Essa linha:
        // - Cria um DbContextOptions<AppDbContext>
        // - Passa isso automaticamente pro AppDbContext
        // - E o AppDbContext envia para o DbContext (base class) com : base(options)
        // ============================================================

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}
