using Application.DTOs.Requests;
using Application.UseCases.autenticacao;
using AutoMapper;
using Domain.Repositories.UsuarioRepository;
using Application.Shared;
using Domain.Exceptions;
using Application.Services.Token;
using Domain;
using Application.Services.Criptografia;

namespace Application.UseCases.Autenticacao
{
    public class AutenticacaoUseCase : IAutenticacaoUseCase
    {
        private readonly IUsuarioRepository _repository;

        private readonly IMapper _mapper;
        

        private readonly IServicoDeToken _servicoDeToken;

        private readonly ICriptografiaDeSenha _criptoGrafiaDeSenha;


        public AutenticacaoUseCase(IUsuarioRepository repository, IMapper mapper, IServicoDeToken servicoDeToken, ICriptografiaDeSenha criptoGrafiaDeSenha)
        {
            _repository = repository;
            _mapper = mapper;
            _servicoDeToken = servicoDeToken;
            _criptoGrafiaDeSenha = criptoGrafiaDeSenha;
        }

        public  async Task<RespostaDeSucessoDaApi<UsuarioAutenticado>> Execute(AutenticacaoRequest request)
        {
            var usuarioLogado = await Autenticar(request);

            return new RespostaDeSucessoDaApi<UsuarioAutenticado>
            {
                Succes = true,
                Message = "Usuario logado com sucesso.",
                Data = usuarioLogado
            };
            
        }

        private async Task<UsuarioAutenticado> Autenticar(AutenticacaoRequest request)
        {
          var senhaCriptografada = _criptoGrafiaDeSenha.CriptografarSenha(request.Senha);
          var usuario = await _repository.ObterPorEmail(request.Email);
          
          if(usuario == null || usuario.Senha != senhaCriptografada)
          {
            throw new ErroDeValidacaoException([MensagensDeExceptionAutenticacao.CREDENCIAIS_INVALIDAS]);
          }

           var token = _servicoDeToken.GerarToken(usuario.Id, usuario.Email, usuario.Administrador);
           var dataExpiracaotoken = _servicoDeToken.ObterDataExpiracaoToken(token);
           var usuarioAutenticado = new UsuarioAutenticado();

           usuarioAutenticado.Nome = usuario.Nome;
           usuarioAutenticado.Email = usuario.Email;
           usuarioAutenticado.Token = token;
           usuarioAutenticado.ExpiraEm = dataExpiracaotoken;

           return usuarioAutenticado;
           
        }
    }
}