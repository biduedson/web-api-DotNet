

using Application.DTOs;
using Domain.Entities;
using Domain.Repositories.EquipamentoRepository;

namespace Application.UseCases.CriarEquipamento;

public class CriarEquipamentoUseCase
{
 private readonly IEquipamentoRepository _repository;

 public CriarEquipamentoUseCase(IEquipamentoRepository repository)
 {
    _repository = repository;
 }

 public async Task ExecuteAsync(EquipamentoDTO dto)
 {
    var equipamento = new Equipamento(dto.Nome, dto.Descricao);
    await _repository.AdicionarAsync(equipamento);
 }
}