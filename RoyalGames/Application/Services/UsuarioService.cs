using Microsoft.IdentityModel.Tokens;
using RoyalGames.Domains;
using RoyalGames.DTOs.UsuarioDto;
using RoyalGames.Interfaces;
using System.Security.Cryptography;
using System.Text;
using VHBurguer.Exceptions;

namespace RoyalGames.Application.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public List<LerUsuarioDto> GetUsuario()
        {
            List<Usuario> usuarios = _repository.GetUsuarios();
            List<LerUsuarioDto> usuariosDto = usuarios.Select(user => LerDto(user)).ToList();

            return usuariosDto;
        }

        public LerUsuarioDto GetUsuarioById(int id)
        {
            Usuario usuario = _repository.GetUsuarioById(id);

            if (usuario == null) throw new DomainException("Usuário não existe");

            return LerDto(usuario);
        }

        private static LerUsuarioDto LerDto(Usuario usuario)
        {
            LerUsuarioDto usuarioBanco = new LerUsuarioDto
            {
                UsuarioID = usuario.UsuarioID,
                Nome = usuario.Nome,
                Email = usuario.Email,
                AdminUsuario = usuario.AdminUsuario,
                StatusUsuario = usuario.StatusUsuario,
            };

            return usuarioBanco;
        }

        public void ValidarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            {
                throw new Exception("Email inválido");
            }
        }

        public void ValidarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new Exception("Nome inválido");
            }
        }

        private static byte[] HashSenha(string senha)
        {
            if (string.IsNullOrEmpty(senha)) throw new DomainException("O campo senha é obrigatório");

            using var shar256 = SHA256.Create();
            return shar256.ComputeHash(Encoding.UTF8.GetBytes(senha));
        }

        public LerUsuarioDto Adicionar(CriarUsuarioDto usuarioDto)
        {
            ValidarEmail(usuarioDto.Email);
            ValidarNome(usuarioDto.Nome);

            if (_repository.EmailExiste(usuarioDto.Email)) throw new DomainException("Este email já está cadastrado");

            Usuario usuario = new Usuario
            {
                Email = usuarioDto.Email,
                Nome = usuarioDto.Nome,
                Senha = HashSenha(usuarioDto.Senha),
                StatusUsuario = true,
                AdminUsuario = false,
            };

            _repository.Adicionar(usuario);
            return LerDto(usuario);
        }

        public void Remover(int id)
        {
            Usuario? usuario = _repository.GetUsuarioById(id);

            if (usuario == null) throw new DomainException("Usuario não existe");

            _repository.Remover(id);
        }

        public LerUsuarioDto Atualizar(CriarUsuarioDto usuarioDto, int id)
        {
            Usuario? usuarioBanco = _repository.GetUsuarioById(id);

            if (usuarioBanco == null) throw new DomainException("Usuário não encontrado");

            _repository.EmailExiste(usuarioBanco.Email);

            Usuario? usuarioComMesmoEmail = _repository.GetUsuarios().FirstOrDefault(user => user.Email == usuarioDto.Email);

            if (usuarioComMesmoEmail != null && usuarioComMesmoEmail.UsuarioID != id) throw new DomainException("Este email já foi cadastrado em outra conta");

            usuarioBanco.Nome = usuarioDto.Nome;
            usuarioBanco.Senha = HashSenha(usuarioDto.Senha);
            usuarioBanco.Email = usuarioDto.Email;

            _repository.Atualizar(usuarioBanco);

            return LerDto(usuarioBanco);
        }
    }
}
