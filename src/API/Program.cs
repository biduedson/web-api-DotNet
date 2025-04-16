  
using DotNetEnv;
using Application;
using API.Filtros;
using Infrastructure;


var builder = WebApplication.CreateBuilder(args);

Env.Load();

// Carrega variáveis de ambiente e configurações de arquivos (appsettings.json)
builder.Configuration.AddEnvironmentVariables();

// Adiciona o arquivo appsettings.json (se necessário)
builder.Configuration.AddJsonFile("appsettings.Development", optional: true, reloadOnChange: true);




builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AdicionarApplication(builder.Configuration);
builder.Services.AdicionarInfrastructure(builder.Configuration);
builder.Services.AddMvc(options => options.Filters.Add(typeof(FiltroDeException)));




var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();