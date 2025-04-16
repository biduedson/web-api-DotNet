
namespace Application.Services.Token;

public interface IServicoDeToken
{
string GerarToken(Guid idUsuario, string email, bool administrador);
bool ValidarToken(string token);
Guid? ObterIdDoUsuarioDoToken(string token);
DateTime ObterDataExpiracaoToken(string token);
}