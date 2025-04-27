using Application.DTOs.Requests;
using AutoMapper;
using Domain.Repositories.UsuarioRepository;
using Application.Shared;
using Domain.Exceptions;
using Application.Services.Token;
using Domain;
using Application.Services.Criptografia;
using Application.Http;

namespace Application.UseCases.Autenticacao
{
    public class AutenticacaoUseCase : IAutenticacaoUseCase
    {
        private readonly IUsuarioRepository _repository;
        private readonly IMapper _mapper;
        private readonly IServicoDeToken _servicoDeToken;
        private readonly ICriptografiaDeSenha _criptoGrafiaDeSenha;

        private readonly IRespostasDaApi<UsuarioAutenticado> _respostasDaApi;

        /// <summary>
        /// Inicializa a classe com as dependências necessárias para autenticação de usuários.
        /// </summary>
        /// <param name="repository">Repositório de usuários para realizar operações de banco de dados.</param>
        /// <param name="mapper">Objeto para mapear entidades para DTOs.</param>
        /// <param name="servicoDeToken">Serviço para geração de tokens de autenticação.</param>
        /// <param name="criptoGrafiaDeSenha">Serviço para criptografar senhas de usuários.</param>
        public AutenticacaoUseCase(
            IUsuarioRepository repository,
            IMapper mapper,
            IServicoDeToken servicoDeToken,
            ICriptografiaDeSenha criptoGrafiaDeSenha,
            IRespostasDaApi<UsuarioAutenticado> respostasDaApi
            )
        {
            _repository = repository;
            _mapper = mapper;
            _servicoDeToken = servicoDeToken;
            _criptoGrafiaDeSenha = criptoGrafiaDeSenha;
            _respostasDaApi = respostasDaApi;
        }

        /// <summary>
        /// Realiza o processo de autenticação de um usuário com base no request fornecido.
        /// - Valida as credenciais do usuário.
        /// - Se as credenciais forem válidas, gera um token de autenticação.
        /// - Retorna uma resposta de sucesso com os dados do usuário e o token gerado.
        /// </summary>
        /// <param name="request">Objeto contendo o e-mail e a senha fornecidos pelo usuário.</param>
        /// <returns>Resposta de sucesso contendo os dados do usuário autenticado e o token gerado.</returns>
        public async Task<IRespostasDaApi<UsuarioAutenticado>> Execute(AutenticacaoRequest request)
        {
            var usuarioLogado = await Autenticar(request);

            return _respostasDaApi.Ok(usuarioLogado, "Usuario logado com sucesso.");


        }

        /// <summary>
        /// Realiza a autenticação do usuário.
        /// - Criptografa a senha fornecida.
        /// - Verifica se o usuário existe e se a senha está correta.
        /// - Gera um token de autenticação e retorna os dados do usuário autenticado.
        /// </summary>
        /// <param name="request">Objeto contendo o e-mail e a senha fornecidos pelo usuário.</param>
        /// <returns>Dados do usuário autenticado, incluindo o token gerado e a data de expiração.</returns>
        private async Task<UsuarioAutenticado> Autenticar(AutenticacaoRequest request)
        {

            var senhaCriptografada = _criptoGrafiaDeSenha.CriptografarSenha(request.Senha);
            var usuario = await _repository.ObterPorEmail(request.Email);

            if (usuario == null || usuario.Senha != senhaCriptografada)
            {
                throw new ErroDeValidacaoException([MensagensDeExceptionAutenticacao.CREDENCIAIS_INVALIDAS]);
            }

            var token = _servicoDeToken.GerarToken(usuario.Id, usuario.Email, usuario.Administrador);
            var dataExpiracaoToken = _servicoDeToken.ObterDataExpiracaoToken(token);

            var usuarioAutenticado = new UsuarioAutenticado
            {
                Nome = usuario.Nome,
                Email = usuario.Email,
                Token = token,
                ExpiraEm = dataExpiracaoToken
            };

            return usuarioAutenticado;
        }
    }
}
