namespace Domain.Entities
{
    /// <summary>
    /// A classe <see cref="Usuario"/> representa uma entidade no domínio da aplicação que
    /// mapeia os dados do usuário, como suas informações pessoais e status.
    /// 
    /// <para>
    /// A entidade <see cref="Usuario"/> tem um identificador único <see cref="Id"/>, um nome, 
    /// e-mail, senha, e status que indica se o usuário está ativo ou não. Além disso, a classe 
    /// possui uma relação com a entidade <see cref="ReservaDeEquipamento"/>, indicando que um usuário
    /// pode fazer várias reservas de equipamentos.
    /// </para>
    /// </summary>
    public class Usuario
    {
        /// <summary>
        /// Identificador único do usuário.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nome do usuário.
        /// </summary>
        public string Nome { get; set; } = string.Empty;

        /// <summary>
        /// E-mail do usuário, usado para autenticação e identificação.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Senha do usuário, usada para autenticação no sistema.
        /// </summary>
        public string Senha { get; set; } = string.Empty;

        /// <summary>
        /// Status que indica se o usuário está ativo no sistema.
        /// </summary>
        public bool Ativo { get; set; } = true;

        /// <summary>
        /// Data de criação do usuário. Geralmente registrada no momento da criação da conta.
        /// </summary>
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Indica se o usuário é um administrador do sistema. Por padrão, é falso.
        /// </summary>
        public bool Administrador { get; set; } = false;

        /// <summary>
        /// Lista de reservas feitas pelo usuário, representando o relacionamento com a entidade <see cref="ReservaDeEquipamento"/>.
        /// </summary>
        public List<ReservaDeEquipamento> Reservas { get; set; } = new();

        /// <summary>
        /// Método que torna o usuário um administrador do sistema.
        /// </summary>
        public void TornarUsuarioAdmin() => Administrador = true;
    }
}
