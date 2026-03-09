using RoyalGames.Application.Autenticacao;
using RoyalGames.Domains;
using RoyalGames.DTOs.AutenticacaoDto;
using RoyalGames.Interfaces;
using VHBurguer.Exceptions;

namespace RoyalGames.Application.Services
{
    public class AutenticacaoService
    {
        private readonly IUsuarioRepository _repository;
        private readonly GeradorTokenJwt _geradorTokenJwt;

        public AutenticacaoService(IUsuarioRepository repository, GeradorTokenJwt geradorTokenJwt)
        {
            _repository = repository;
            _geradorTokenJwt = geradorTokenJwt;
        }

        private static bool VerificarSenha(string senhaDigitada, byte[] hashSenha)
        {
            using var sha = System.Security.Cryptography.SHA256.Create();
            
            var senhaHashDigitada = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senhaDigitada));

            return senhaHashDigitada.SequenceEqual(hashSenha);
        }

        public TokenDto GetToken(LoginDto usuario) { 
            Usuario? usuarioBanco = _repository.GetUsuarios().FirstOrDefault(user => user.Email == usuario.Email);

            if (usuarioBanco == null) throw new DomainException("Email inválido ou senha inválido");

            if (VerificarSenha(usuario.Senha, usuarioBanco.Senha) == false) throw new DomainException("Senha inválido");

            if (usuarioBanco.StatusUsuario == false) throw new DomainException("Conta desativada");

            var token = _geradorTokenJwt.GerarToken(usuarioBanco);

            TokenDto tokenDto = new TokenDto() { Token = token};

            return tokenDto;
        }
    }
}
