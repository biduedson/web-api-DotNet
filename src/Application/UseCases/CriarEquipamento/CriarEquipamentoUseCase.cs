// Importações necessárias
using Application.DTOs;
using Application.DTOs.Requests; // DTO de entrada (request)
using Application.DTOs.Responses; // DTO de saída (response)
using AutoMapper; // Biblioteca AutoMapper para mapear objetos
using Domain.Entities; // Entidade de domínio Equipamento
using Domain.Repositories.EquipamentoRepository; // Interface do repositório

namespace Application.UseCases.CriarEquipamento;

// A classe CriarEquipamentoUseCase implementa a interface ICriarEquipamentoUseCase
// Isso define o contrato que a aplicação usa para criar equipamentos
public class CriarEquipamentoUseCase : ICriarEquipamentoUseCase
{
    // Campo privado que guarda a instância do repositório de equipamentos (injeção de dependência)
    private readonly IEquipamentoRepository _repository;

    // Instância do AutoMapper que será usada para transformar DTOs em entidades
    public IMapper _mapper;

    // Construtor da classe, recebe via injeção de dependência:
    // - o repositório que lida com persistência
    // - o mapper que converte DTOs em entidades
    public CriarEquipamentoUseCase(IEquipamentoRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    // Método principal da classe: Executa o caso de uso de criação de um equipamento
    public async Task<EquipamentoRegistradoResponse> Execute(RegistraEquipamentoRequest request)
    {
        // Usa o AutoMapper para converter o DTO de request para a entidade Equipamento
        var equipamento = _mapper.Map<Equipamento>(request);

        // Persiste o equipamento no banco de dados usando o repositório
        await _repository.AdicionarEquipamentoAsync(equipamento);

        // Retorna um DTO de resposta indicando que o equipamento foi registrado
        return new EquipamentoRegistradoResponse
        {
            Nome = request.Nome
        };
    }
}
