//------------------------------------------------------------------------------
// <auto-generated>
//     Código gerado manualmente para acessar mensagens de erro do recurso .resx.
//     Este arquivo está relacionado ao usuário e suas mensagens de validação.
// </auto-generated>
//------------------------------------------------------------------------------


using System.Resources;

// Namespace necessário para gerenciar recursos localizados
namespace Domain
{
    // Indica que este código foi gerado manualmente (simulando ferramenta)
    [System.CodeDom.Compiler.GeneratedCode("Manual", "1.0.0")]

    // Ignora esse código no depurador (para facilitar o debug do projeto principal)
    [System.Diagnostics.DebuggerNonUserCode]

    // Indica que essa classe é gerada por ferramenta
    [System.Runtime.CompilerServices.CompilerGenerated]
    public class MensagensDeExceptionAutorizacao
    {
        // Responsável por acessar os dados do .resx (ResourceManager do .NET)
        private static ResourceManager resourceMan;

        // Construtor privado para evitar instanciamento externo
        internal MensagensDeExceptionAutorizacao() { }

        /// <summary>
        /// Retorna a instância do ResourceManager para este recurso.
        /// </summary>
        public static ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    // Nome deve ser o caminho completo (namespace + nome do .resx)
                    resourceMan = new ResourceManager("Domain.Recursos.Autorizacao.MensagensDeExceptionAutorizacao", typeof(MensagensDeExceptionAutorizacao).Assembly);
                }
                return resourceMan;
            }
        }

        /// <summary>
        /// Retorna a mensagem: "Voce não tem permição para acessar este recurso.."
        /// </summary>
        public static string ACESSO_NEGADO
        {
            get
            {
                return ResourceManager.GetString("ACESSO_NEGADO");
            }
        }
    }
}
