using RoyalGames.Domains;

namespace RoyalGames.Interfaces
{
    public interface IUsuarioRepository
    {
        List<Usuario> GetUsuarios();
        Usuario? GetUsuarioById(int id);
        bool? ValidarAdmin(int id);
        bool EmailExiste(string email);
        void Adicionar(Usuario usuario);
        void Remover(int id);
        void Atualizar(Usuario usuario);
    }
}
