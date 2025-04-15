
namespace Application.Services.Jwts;

public interface IServicoDeTokensJwt{
string Gerartoken(Guid IdUsuario, string email, string role);
bool ValidarToken(string token);
Guid? ObterIdDoUsuarioDoToken(string token);
}