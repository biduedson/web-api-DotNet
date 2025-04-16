namespace Application.Shared
{
        public class UsuarioAutenticado
{
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiraEm { get; set; }
}
}

