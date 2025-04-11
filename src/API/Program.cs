using Application.UseCases.CriarEquipamento;
using Domain.Repositories.EquipamentoRepository;
using Infrastructure.Data.Context;
using Infrastructure.Data.Repositories;


using Microsoft.EntityFrameworkCore;
using Application;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AdicionarApplication(builder.Configuration);

// DB

var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));


//inm=jeçao de dedpendencia

builder.Services.AddScoped<IEquipamentoRepository, EquipamentoRepository>();
builder.Services.AddScoped<CriarEquipamentoUseCase>();

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();