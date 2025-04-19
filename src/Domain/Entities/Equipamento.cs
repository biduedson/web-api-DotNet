namespace Domain.Entities
{
    /// <summary>
    /// A classe <see cref="Equipamento"/> representa uma entidade no domínio da aplicação.
    /// 
    /// <para>
    /// Entidades são objetos que possuem uma identidade única, representada geralmente por um identificador,
    /// como um <see cref="Guid"/>. Elas são fundamentais para o domínio do sistema e frequentemente correspondem
    /// a tabelas no banco de dados, onde cada instância da entidade representa uma linha da tabela.
    /// </para>
    /// 
    /// <para>
    /// No contexto dessa aplicação, a entidade <see cref="Equipamento"/> representa um equipamento que pode ser
    /// reservado, com um nome e uma descrição associados. A classe possui métodos que alteram suas propriedades,
    /// como <see cref="AtualizarDescricao"/> e <see cref="AtualizarNome"/>.
    /// </para>
    /// </summary>
    public class Equipamento
    {
        /// <summary>
        /// Identificador único do equipamento.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nome do equipamento.
        /// </summary>
        public string Nome { get; set; } = string.Empty;

        /// <summary>
        /// Descrição do equipamento.
        /// </summary>
        public string Descricao { get; set; } = string.Empty;

        /// <summary>
        /// Lista de reservas associadas ao equipamento. Cada reserva é uma instância da classe <see cref="ReservaDeEquipamento"/>.
        /// </summary>
        public List<ReservaDeEquipamento> Reservas { get; set; } = new();

        /// <summary>
        /// Atualiza a descrição do equipamento.
        /// </summary>
        /// <param name="descricao">Nova descrição para o equipamento.</param>
        public void AtualizarDescricao(string descricao)
        {
            Descricao = descricao;
        }

        /// <summary>
        /// Atualiza o nome do equipamento.
        /// </summary>
        /// <param name="nome">Novo nome para o equipamento.</param>
        public void AtualizarNome(string nome)
        {
            Nome = nome;
        }
    }
}
