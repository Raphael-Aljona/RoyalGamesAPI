using Microsoft.AspNetCore.Http.HttpResults;
using RoyalGames.Contexts;
using RoyalGames.Domains;
using RoyalGames.Interfaces;

namespace RoyalGames.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly RoyalGamesContext _context;

        public UsuarioRepository(RoyalGamesContext context)
        {
            _context = context;
        }
        public List<Usuario> GetUsuarios()
        {
            return _context.Usuario.ToList();
        }
        public Usuario? GetUsuarioById(int id)
        {
            Usuario? usuario = _context.Usuario.Find(id);
            return usuario;
        }
        public void Adicionar(Usuario usuario)
        {
            _context.Add(usuario);
            _context.SaveChanges();
        }

        public void Atualizar(Usuario usuario)
        {
            Usuario? usuarioBanco = _context.Usuario.FirstOrDefault(u => u.UsuarioID == usuario.UsuarioID);

            if (usuarioBanco == null) return;

            usuarioBanco.Email = usuario.Email;
            usuarioBanco.Nome = usuario.Nome;

            _context.SaveChanges();
        }
        public void Remover(int id)
        {
            Usuario? usuario = _context.Usuario.FirstOrDefault(user => user.UsuarioID == id);

            if (usuario == null) return;

            _context.Usuario.Remove(usuario);
            _context.SaveChanges();
        }

        public bool? ValidarAdmin(int id)
        {
            Usuario? usuario = _context.Usuario.FirstOrDefault(user => user.UsuarioID == id);
            return usuario?.AdminUsuario;
        }

        public bool EmailExiste(string email)
        {
            return _context.Usuario.Any(u => u.Email == email);
        }
    }
}
