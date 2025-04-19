using System.Security.Cryptography; // Importa o namespace necessário para realizar operações criptográficas como hashing (ex: SHA-512).
using System.Text; // Importa o namespace necessário para manipular codificação de strings (ex: UTF-8) e construir strings com eficiência.
using Application.Services.Criptografia; 
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services.Criptografia
{
    /// <summary>
    /// Classe responsável por realizar a criptografia (hash) de senhas utilizando o algoritmo SHA-512.
    /// Essa técnica é usada para proteger senhas antes de armazená-las no banco de dados, tornando difícil 
    /// para invasores recuperarem as senhas mesmo se tiverem acesso ao banco.
    /// </summary>
    public class CriptografiaDeSenha : ICriptografiaDeSenha
    {
        private readonly IConfiguration _configuration; // Armazena a configuração do aplicativo, como chaves e valores definidos no appsettings.json.
        private readonly string _chaveAdicional; // Chave secreta adicional que será combinada com a senha para aumentar a segurança.

        /// <summary>
        /// Construtor que recebe uma chave adicional externa (salt).
        /// A chave pode ser configurada no appsettings.json ou vinda de uma variável de ambiente.
        /// O salt torna o processo de hashing mais seguro, dificultando ataques de dicionário ou rainbow tables.
        /// </summary>
        /// <param name="configuration">Configuração contendo a chave adicional para o processo de criptografia.</param>
        /// <param name="chaveAdicional">Chave secreta que será combinada com a senha antes de aplicar o hash.</param>
        public CriptografiaDeSenha(IConfiguration configuration, string chaveAdicional)
        {
            _configuration = configuration;
            _chaveAdicional = configuration.GetValue<string>("Settings:Passwords:AdditionalKey")!;
        }

        /// <summary>
        /// Criptografa a senha recebida como parâmetro usando o algoritmo SHA-512 e uma chave adicional.
        /// Essa abordagem é mais segura porque combina a senha com uma chave secreta antes de aplicar o hash.
        /// </summary>
        /// <param name="senha">Senha em texto puro (plain text) fornecida pelo usuário.</param>
        /// <returns>String representando o hash criptografado da senha, em formato hexadecimal.</returns>
        public string CriptografarSenha(string senha)
        {
            // Concatena a senha com a chave adicional (salt) para tornar o hash mais seguro.
            // Isso dificulta a possibilidade de gerar um hash de senha idêntico, mesmo que duas pessoas usem a mesma senha.
            var novaSenha = $"{senha}{_chaveAdicional}";

            // Converte a string concatenada para um array de bytes usando a codificação UTF-8.
            // UTF-8 é uma codificação universal compatível com a maioria dos sistemas e bancos de dados.
            var bytes = Encoding.UTF8.GetBytes(novaSenha);

            // Aplica o algoritmo SHA-512 sobre o array de bytes.
            // O SHA-512 é uma função de hash criptográfica que gera um valor único e fixo para qualquer entrada de dados.
            var hashbytes = SHA512.HashData(bytes);

            // Converte o array de bytes do hash para uma string hexadecimal legível.
            return StringBytes(hashbytes);
        }

        /// <summary>
        /// Converte um array de bytes (como o resultado de um hash) em uma string hexadecimal.
        /// Exemplo: [171, 14, 255] => "ab0eff"
        /// Esse processo é necessário para armazenar o hash de forma legível e comparável em texto.
        /// </summary>
        /// <param name="bytes">Array de bytes resultante do algoritmo de hash.</param>
        /// <returns>String com a representação hexadecimal dos bytes.</returns>
        private static string StringBytes(byte[] bytes)
        {
            // StringBuilder é utilizado para concatenar strings de forma eficiente, principalmente em loops.
            var sb = new StringBuilder();

            // Itera sobre cada byte do array e converte-o para seu valor hexadecimal correspondente.
            foreach (var b in bytes)
            {
                // ToString("x2") converte o byte para uma string hexadecimal de dois dígitos.
                var hex = b.ToString("x2");
                sb.Append(hex); // Adiciona o valor hexadecimal ao StringBuilder.
            }

            // Retorna a string hexadecimal final que representa o hash completo.
            return sb.ToString();
        }
    }
}
