namespace Domain.Exceptions;

public class ErroDeValidacaoException : DomainException
{
    public IList<string> MenssagensDeErro {get; set;}

    public ErroDeValidacaoException(IList<string> menssagensDeErro)
    {
        MenssagensDeErro = menssagensDeErro;
    }
}