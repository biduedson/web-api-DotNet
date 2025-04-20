using DotNetEnv;
using Application;
using API.Filtros;
using Infrastructure;
using API;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

// Carrega variáveis de ambiente e configurações de arquivos (appsettings.json)
builder.Configuration.AddEnvironmentVariables();

// Adiciona o arquivo appsettings.json (se necessário)
builder.Configuration.AddJsonFile("appsettings.Development", optional: true, reloadOnChange: true);

builder.Services.AdicionarApplication(builder.Configuration);
builder.Services.AdicionarInfrastructure(builder.Configuration);
builder.Services.AdicionarServicosDaAPI(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Adiciona filtro de exceção
builder.Services.AddMvc(options => options.Filters.Add(typeof(FiltroDeException)));

var app = builder.Build();

// Configurações de Swagger agora são tratadas no método AdicionarServicosDaAPI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("PoliticaCORS"); // Adicione esta linha para usar a política de CORS configurada
app.UseAuthentication(); // Adicione esta linha para usar autenticação
app.UseAuthorization();
app.MapControllers();
app.Run();