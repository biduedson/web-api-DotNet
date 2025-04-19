using Domain;
using Application.DTOs.Requests;
using FluentValidation;

namespace Application.UseCases.CriarEquipamento.Validadores
{
    /// <summary>
    /// Classe responsável por validar os dados recebidos para criação de um novo equipamento.
    /// </summary>
    /// <remarks>
    /// Esta classe herda de <see cref="AbstractValidator{T}"/> para definir regras de validação específicas
    /// aplicadas ao DTO <see cref="RegistraEquipamentoRequest"/>.
    /// 
    /// As validações são utilizadas para garantir que os campos obrigatórios estejam presentes
    /// e que os dados estejam em conformidade com as regras de negócio antes de prosseguir com a lógica do caso de uso.
    /// </remarks>
    public class ValidadorDeCriacaoDeEquipamento : AbstractValidator<RegistraEquipamentoRequest>
    {
        /// <summary>
        /// Construtor que define todas as regras de validação aplicáveis ao DTO de criação de equipamento.
        /// </summary>
        public ValidadorDeCriacaoDeEquipamento()
        {
            /// <summary>
            /// Regra que define que o campo 'Nome' não pode ser vazio.
            /// </summary>
            /// <remarks>
            /// Caso o campo esteja vazio, será retornada a mensagem de erro definida em <see cref="MensagensDeExceptionEquipamento.NOME_DE_EQUIPAMENTO_OBRIGATORIO"/>.
            /// </remarks>
            RuleFor(equipamento => equipamento.Nome)
                .NotEmpty()
                .WithMessage(MensagensDeExceptionEquipamento.NOME_DE_EQUIPAMENTO_OBRIGATORIO);

            /// <summary>
            /// Regra que define que o campo 'Descricao' não pode ser vazio.
            /// </summary>
            /// <remarks>
            /// Caso o campo esteja vazio, será retornada a mensagem de erro definida em <see cref="MensagensDeExceptionEquipamento.DESCRICAO_DE_EQUIPAMENTO_OBRIGATORIO"/>.
            /// </remarks>
            RuleFor(equipamento => equipamento.Descricao)
                .NotEmpty()
                .WithMessage(MensagensDeExceptionEquipamento.DESCRICAO_DE_EQUIPAMENTO_OBRIGATORIO);
        }
    }
}
