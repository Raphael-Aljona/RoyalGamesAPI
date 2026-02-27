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
            _context.Remove(id);
            _context.SaveChanges();
        }
    }
}
