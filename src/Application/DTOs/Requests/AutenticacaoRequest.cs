namespace Application.DTOs.Requests;

public class AutenticacaoRequest
{
    public string Email {get; set;} = string.Empty;
    public string Senha {get; set;} = string.Empty;
}