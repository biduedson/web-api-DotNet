

using Domain;
using Application.DTOs.Requests;
using FluentValidation;

namespace Application.UseCases.CriarEquipamento.Validadores
{
    // Classe que herda de AbstractValidator para criar as regras de validação para o registro de usuário
    public class ValidadorDeCriacaoDeEquipamento : AbstractValidator<RegistraEquipamentoRequest>
    {
        public ValidadorDeCriacaoDeEquipamento()
        {
             // Definindo a regra de validação para o campo 'Nome' do objeto RegistraEquipamentoRequest
             // A regra exige que o campo 'Nome' não seja vazio
             RuleFor(equipamento => equipamento.Nome).NotEmpty().WithMessage(MensagensDeExceptionEquipamento.NOME_DE_EQUIPAMENTO_OBRIGATORIO);

            // Definindo a regra de validação para o campo 'Descricao' do objeto RegistraEquipamentoRequest
             // A regra exige que o campo 'descricao' não seja vazio
             RuleFor(equipamento => equipamento.Descricao).NotEmpty().WithMessage(MensagensDeExceptionEquipamento.DESCRICAO_DE_EQUIPAMENTO_OBRIGATORIO);

        }
    }
}