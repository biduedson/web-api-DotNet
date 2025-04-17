using Microsoft.EntityFrameworkCore.Design; // Necessário para a implementação de fábricas de DbContext em tempo de design.
using DotNetEnv; // Biblioteca usada para carregar variáveis de ambiente de arquivos .env.
using Microsoft.EntityFrameworkCore; // Fornece as funcionalidades do Entity Framework Core.
using System; // Usado para manipulação de exceções e variáveis de ambiente.

namespace Infrastructure.Data.Context
{
    // A classe AppDbContextFactory é necessária para permitir que o EF Core crie uma instância do AppDbContext em tempo de design.
    // Isso é especialmente importante para gerar migrações e executar comandos como 'dotnet ef migrations add' ou 'dotnet ef database update',
    // quando o DbContext não tem um construtor público padrão ou quando a configuração do contexto depende de dados externos, como variáveis de ambiente.

    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        /// <summary>
        /// Este método é necessário para criar uma instância do DbContext em tempo de design, ou seja, quando estamos executando comandos
        /// do Entity Framework Core, como migrações, e o DbContext não possui um construtor público padrão. 
        /// O método é invocado automaticamente durante o processo de migração ou atualização do banco de dados.
        /// </summary>
        /// <param name="args">Argumentos opcionais passados pelo EF Core, normalmente não utilizados aqui.</param>
        /// <returns>Retorna uma instância configurada do AppDbContext.</returns>
        public AppDbContext CreateDbContext(string[] args)
        {
            // Carrega o arquivo .env da pasta 'api' onde estão definidas as variáveis de ambiente, como a string de conexão do banco de dados.
            // Isso é necessário porque a string de conexão é armazenada em uma variável de ambiente e não em um arquivo de configuração como appsettings.json.
            // A variável de ambiente é acessada aqui para configurar o banco de dados dinamicamente.
            Env.Load(@"../../src/api/.env");

            // Recupera a variável de ambiente que contém a string de conexão para o banco de dados. A chave "ConnectionStrings__Default"
            // foi configurada no arquivo .env e é responsável por armazenar a string de conexão do banco de dados.
            var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__Default") ?? 
                throw new InvalidOperationException("String de conexão vazia.");

            // Configura o DbContextOptionsBuilder para usar o MySQL, com a string de conexão recuperada.
            // O uso do MySQL é configurado automaticamente pelo EF Core com a versão do servidor detectada.
            var optionBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            // Retorna uma instância do AppDbContext com as opções configuradas.
            // O DbContext criado aqui será usado pelos comandos do EF Core para executar migrações ou outras operações.
            return new AppDbContext(optionBuilder.Options);
        }
    }
}

/* Passos e Comandos:

1. **Instalar a ferramenta dotnet-ef globalmente (se necessário)**:
    Se você ainda não tem o EF Core instalado globalmente, você pode instalar a ferramenta usando o seguinte comando no terminal:

    ```bash
    dotnet tool install --global dotnet-ef
    ```

    Esse comando instala o `dotnet-ef` globalmente em seu sistema, permitindo que você utilize os comandos do Entity Framework Core, como `migrations` e `database update`, em qualquer projeto.

2. **Verificar a versão instalada do dotnet-ef**:
    Após a instalação, você pode verificar se a ferramenta foi instalada corretamente e qual versão está em uso:

    ```bash
    dotnet ef --version
    ```

    Isso garante que a ferramenta foi instalada corretamente e que você está usando a versão desejada do `dotnet-ef`.

3. **Gerar a primeira migração**:
    Depois de configurar o `AppDbContextFactory` (como mostrado no código acima) e ter certeza de que o `.env` está corretamente carregando as variáveis de ambiente, você pode gerar uma migração. No terminal, execute o seguinte comando para adicionar a migração:

    ```bash
    dotnet ef migrations add NomeDaMigração
    ```

    **Substitua `NomeDaMigração`** por um nome descritivo que represente a migração (ex: `CreateInitialSchema`).

    Esse comando gera a migração, que é um conjunto de instruções que descreve como o banco de dados deve ser alterado, como a criação de tabelas e a adição de colunas.

4. **Aplicar a migração ao banco de dados**:
    Após gerar a migração, aplique as alterações no banco de dados com o seguinte comando:

    ```bash
    dotnet ef database update
    ```

    Esse comando aplica as migrações pendentes ao banco de dados, criando as tabelas e atualizando o esquema de acordo com as migrações geradas.

5. **Verificar se tudo foi configurado corretamente**:
    Depois de rodar esses comandos, você pode verificar se a migração foi aplicada corretamente, verificando o banco de dados ou executando consultas para ver se as tabelas foram criadas.

*/
