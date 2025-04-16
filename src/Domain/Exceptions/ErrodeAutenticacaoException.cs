namespace Domain.Exceptions;

public  class ErroDeAutenticacaoException : DomainException
{
    public string MenssagenDeErro {get; set;}
    public ErroDeAutenticacaoException(string mensagemDeErro)
    {
            MenssagenDeErro = mensagemDeErro;
    } 

}