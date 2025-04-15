// Importações
using AutoMapper; // Biblioteca responsável pelo mapeamento entre classes
using Application.DTOs.Requests; // Classe RegistraEquipamentoRequest
using Domain.Entities; // Entidade Equipamento

namespace Application.Services.AutoMapper;

// Classe AutoMapping herda de Profile, que é uma classe base do AutoMapper
// Essa herança é necessária para configurar os mapeamentos
public class AutoMapping : Profile
{
    // Construtor da classe AutoMapping
    // O AutoMapper irá chamar esse construtor para registrar os mapeamentos
    public AutoMapping()
    {
        // Aqui fazemos o mapeamento de RegistraEquipamentoRequest para Equipamento
        // Isso diz ao AutoMapper como transformar um DTO de request em uma entidade
        CreateMap<RegistraEquipamentoRequest, Equipamento>();
        CreateMap<RegistrarUsuarioRequest, Usuario>();
    }
}
