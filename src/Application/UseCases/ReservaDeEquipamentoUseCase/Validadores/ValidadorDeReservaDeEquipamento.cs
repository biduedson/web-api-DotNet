using Application.DTOs.Requests;
using Domain;
using FluentValidation;

namespace Application.UseCases.ReservaDeEquipamentoUseCase.Validadores
{
    /// <summary>
    /// Classe responsável por validar os dados recebidos para a criação de uma reserva de equipamento.
    /// </summary>
    /// <remarks>
    /// Esta classe herda de <see cref="AbstractValidator{T}"/> e define regras de validação para o DTO
    /// <see cref="ReservaDeEquipamentoRequest"/>, assegurando que todos os dados obrigatórios estejam presentes
    /// e em conformidade com as regras de negócio estabelecidas.
    /// </remarks>
    public class ValidadorDereservaDeEquipamento : AbstractValidator<ReservaDeEquipamentoRequest>
    {
        /// <summary>
        /// Construtor responsável por definir as regras de validação para o objeto <see cref="ReservaDeEquipamentoRequest"/>.
        /// </summary>
        public ValidadorDereservaDeEquipamento()
        {
            /// <summary>
            /// Regra que define que o campo 'UsuarioId' não pode ser vazio.
            /// </summary>
            /// <remarks>
            /// Garante que o identificador do usuário esteja presente. Caso contrário, retorna a mensagem definida
            /// em <see cref="MensagensDeExceptionReservaDeEquipamento.ID_USUARIO_OBRIGATORIA"/>.
            /// </remarks>
            RuleFor(reserva => reserva.UsuarioId)
                .NotEmpty()
                .WithMessage(MensagensDeExceptionReservaDeEquipamento.ID_USUARIO_OBRIGATORIA);

            /// <summary>
            /// Regra que define que o campo 'EquipamentoId' não pode ser vazio.
            /// </summary>
            /// <remarks>
            /// Garante que o identificador do equipamento esteja presente. Caso contrário, retorna a mensagem definida
            /// em <see cref="MensagensDeExceptionReservaDeEquipamento.ID_EQUIPAMENTO_OBRIGATORIA"/>.
            /// </remarks>
            RuleFor(reserva => reserva.EquipamentoId)
                .NotEmpty()
                .WithMessage(MensagensDeExceptionReservaDeEquipamento.ID_EQUIPAMENTO_OBRIGATORIA);

            /// <summary>
            /// Regra que define que o campo 'DataDaReserva' não pode ser vazio.
            /// </summary>
            /// <remarks>
            /// Garante que a data da reserva seja informada. Caso contrário, retorna a mensagem definida
            /// em <see cref="MensagensDeExceptionReservaDeEquipamento.DATA_OBRIGATORIA"/>.
            /// </remarks>
            RuleFor(reserva => reserva.DataDaReserva)
                .NotEmpty()
                .WithMessage(MensagensDeExceptionReservaDeEquipamento.DATA_OBRIGATORIA);

            /// <summary>
            /// Regra que define que o campo 'Status' da reserva não pode ser vazio.
            /// </summary>
            /// <remarks>
            /// Garante que o status da reserva seja informado. Caso contrário, retorna a mensagem definida
            /// em <see cref="MensagensDeExceptionReservaDeEquipamento.STATUS_RESERVA_OBRIGATORIA"/>.
            /// </remarks>
            RuleFor(reserva => reserva.Status)
                .NotEmpty()
                .WithMessage(MensagensDeExceptionReservaDeEquipamento.STATUS_RESERVA_OBRIGATORIA);
        }
    }
}
