namespace Domain.Exceptions;

public class ErroDeAutorizacaoException : DomainException
{
   public string MenssagenDeErro {get; set;}

   public ErroDeAutorizacaoException(string menssagenDeErro)
   {
    MenssagenDeErro = menssagenDeErro;
   }
}