namespace Domain.Exceptions
{
    /// <summary>
    /// A <see cref="DomainException"/> é uma exceção personalizada que herda de <see cref="SystemException"/>.
    /// Ela pode ser utilizada para representar erros específicos que ocorrem dentro do domínio da aplicação.
    /// </summary>
    public class DomainException : SystemException
    {
        // Aqui não há lógica adicional na classe DomainException, mas ela pode ser expandida
        // para incluir mais funcionalidades como mensagens de erro personalizadas, códigos de erro, etc.
    }
}
