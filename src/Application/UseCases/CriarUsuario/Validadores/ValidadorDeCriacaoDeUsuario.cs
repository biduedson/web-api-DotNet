using Application.DTOs.Requests;
using Domain;
using FluentValidation;

namespace Application.UseCases.CriarUsuario.Validadores
{
    /// <summary>
    /// Classe responsável por validar os dados recebidos para o registro de um novo usuário.
    /// </summary>
    /// <remarks>
    /// Esta classe herda de <see cref="AbstractValidator{T}"/> e define regras de validação aplicáveis ao DTO
    /// <see cref="RegistrarUsuarioRequest"/> utilizado na criação de usuários.
    /// 
    /// As regras de validação garantem que os dados obrigatórios estejam preenchidos corretamente e em conformidade
    /// com as regras de negócio da aplicação.
    /// </remarks>
    public class ValidadorDeCriacaoDeUsuario : AbstractValidator<RegistrarUsuarioRequest>
    {
        /// <summary>
        /// Construtor que define as regras de validação para o registro de usuário.
        /// </summary>
        public ValidadorDeCriacaoDeUsuario()
        {
            /// <summary>
            /// Regra que define que o campo 'Nome' não pode ser vazio.
            /// </summary>
            /// <remarks>
            /// Caso esteja vazio, retorna a mensagem definida em <see cref="MensagensDeExceptionUsuario.NOME_OBRIGATORIO"/>.
            /// </remarks>
            RuleFor(usuario => usuario.Nome)
                .NotEmpty()
                .WithMessage(MensagensDeExceptionUsuario.NOME_OBRIGATORIO);

            /// <summary>
            /// Regra que define que o campo 'Email' não pode ser vazio.
            /// </summary>
            /// <remarks>
            /// Caso esteja vazio, retorna a mensagem definida em <see cref="MensagensDeExceptionUsuario.EMAIL_OBRIGATORIO"/>.
            /// </remarks>
            RuleFor(usuario => usuario.Email)
                .NotEmpty()
                .WithMessage(MensagensDeExceptionUsuario.EMAIL_OBRIGATORIO);

            /// <summary>
            /// Regra que define que o campo 'Senha' deve conter no mínimo 6 caracteres.
            /// </summary>
            /// <remarks>
            /// Caso o tamanho da senha seja inferior a 6, retorna a mensagem definida em <see cref="MensagensDeExceptionUsuario.SENHA_INVALIDA"/>.
            /// </remarks>
            RuleFor(usuario => usuario.Senha.Length)
                .GreaterThanOrEqualTo(6)
                .WithMessage(MensagensDeExceptionUsuario.SENHA_INVALIDA);
        }
    }
}
