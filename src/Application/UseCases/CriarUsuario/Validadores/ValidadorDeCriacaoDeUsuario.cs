
using Application.DTOs.Requests;
using Domain;
using FluentValidation;

namespace Application.UseCases.CriarUsuario.Validadores
{
    // Classe que herda de AbstractValidator para criar as regras de validação para o registro de usuário
    public class ValidadorDeCriacaoDeUsuario : AbstractValidator<RegistrarUsuarioRequest>
    {
        public ValidadorDeCriacaoDeUsuario()
        {
            // Definindo a regra de validação para o campo 'Nome' do objeto RegistrarUsuarioRequest
            // A regra exige que o campo 'Nome' não seja vazio
            RuleFor(usuario => usuario.Nome).NotEmpty().WithMessage(MensagensDeExceptionUsuario.NOME_OBRIGATORIO);

            // Definindo a regra de validação para o campo 'email' do objeto RegistrarUsuarioRequest
            // A regra exige que o campo 'Email' não seja vazio
            RuleFor(usuario => usuario.Email).NotEmpty().WithMessage(MensagensDeExceptionUsuario.EMAIL_OBRIGATORIO);


            // Definindo a regra de validação para o campo 'Senha' do objeto RegistrarUsuarioRequest
            // A regra exige que o comprimento da senha seja maior ou igual a 6 caracteres
            RuleFor(usuario => usuario.Senha.Length).GreaterThanOrEqualTo(6).WithMessage(MensagensDeExceptionUsuario.SENHA_INVALIDA);
        }
    }
}