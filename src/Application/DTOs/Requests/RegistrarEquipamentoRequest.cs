namespace Application.DTOs.Requests
{
    /// <summary>
    /// Classe de solicitação para registrar um novo equipamento.
    /// </summary>
    public class RegistraEquipamentoRequest
    {
        /// <summary>
        /// Nome do equipamento a ser registrado.
        /// </summary>
        public string Nome { get; set; } = string.Empty;

        /// <summary>
        /// Descrição detalhada do equipamento.
        /// </summary>
        public string Descricao { get; set; } = string.Empty;
    }
}
