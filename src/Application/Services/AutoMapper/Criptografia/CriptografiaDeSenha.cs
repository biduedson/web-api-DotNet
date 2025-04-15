// Importa o namespace necessário para realizar operações criptográficas como hashing (ex: SHA-512).
using System.Security.Cryptography;

// Importa o namespace necessário para manipular codificação de strings (ex: UTF-8) e construir strings com eficiência.
using System.Text;

namespace Application.criptografia
{
    /// <summary>
    /// Classe responsável por realizar a criptografia (hash) de senhas utilizando o algoritmo SHA-512.
    /// Essa técnica é usada para proteger senhas antes de armazená-las no banco de dados.
    /// </summary>
    public class CriptografiaDeSenha
    {
        // Campo privado e somente leitura que armazena uma "chave adicional" (salt).
        // Essa chave torna o hash mais seguro, dificultando ataques como dicionário ou rainbow tables.
        private readonly string _chaveAdicional;

        /// <summary>
        /// Construtor que recebe uma chave adicional externa.
        /// Essa chave pode ser configurada no appsettings.json ou vinda de uma variável de ambiente.
        /// </summary>
        /// <param name="chaveAdicional">Chave secreta que será combinada com a senha antes de aplicar o hash.</param>
        public CriptografiaDeSenha(string chaveAdicional) => _chaveAdicional = chaveAdicional;

        /// <summary>
        /// Criptografa a senha recebida como parâmetro usando SHA-512 + chave adicional.
        /// </summary>
        /// <param name="senha">Senha em texto puro (plain text) fornecida pelo usuário.</param>
        /// <returns>String representando o hash criptografado da senha, em formato hexadecimal.</returns>
        public string Criptografar(string senha)
        {
            // Concatena a senha original com a chave adicional (salt).
            // Isso torna a senha mais imprevisível mesmo que duas pessoas usem a mesma senha.
            var novaSenha = $"{senha}{_chaveAdicional}";

            // Converte a string para um array de bytes usando codificação UTF-8.
            // Explicação:
            //   Encoding.UTF8.GetBytes("abc") => [97, 98, 99]
            // UTF-8 é uma codificação universal e compatível com a maioria dos sistemas e bancos de dados.
            var bytes = Encoding.UTF8.GetBytes(novaSenha);

            // Aplica o algoritmo SHA-512 sobre o array de bytes.
            // O resultado é um novo array de 64 bytes, representando o hash da senha original.
            // SHA512 é uma função de hash criptográfica que sempre gera o mesmo hash para a mesma entrada.
            var hashbytes = SHA512.HashData(bytes);

            // Converte o array de bytes resultante do hash para uma string legível no formato hexadecimal.
            return StringBytes(hashbytes);
        }

        /// <summary>
        /// Converte um array de bytes (como o resultado de um hash) em uma string hexadecimal.
        /// Exemplo: [171, 14, 255] => "ab0eff"
        /// </summary>
        /// <param name="bytes">Array de bytes resultante do algoritmo de hash.</param>
        /// <returns>String com a representação hexadecimal dos bytes.</returns>
        private static string StringBytes(byte[] bytes)
        {
            // StringBuilder é uma classe otimizada para concatenação de strings em loops.
            // Melhor que usar string += dentro de loops, pois é mais performático e econômico em memória.
            var sb = new StringBuilder();

            // Itera sobre cada byte do array para convertê-lo em dois dígitos hexadecimais.
            foreach (var b in bytes)
            {
                // ToString("x2") transforma o byte em uma string hexadecimal com dois dígitos.
                // Ex: byte 255 => "ff", byte 10 => "0a"
                var hex = b.ToString("x2");
                sb.Append(hex);
            }

            // Retorna a string hexadecimal final que representa o hash completo.
            return sb.ToString();
        }
    }
}
