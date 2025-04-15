  
using DotNetEnv;
using Application;
using API.Filtros;
using Infrastructure;


var builder = WebApplication.CreateBuilder(args);

Env.Load();

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