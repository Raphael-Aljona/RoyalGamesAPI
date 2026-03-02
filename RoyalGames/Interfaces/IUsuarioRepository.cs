using RoyalGames.Domains;

namespace RoyalGames.Interfaces
{
    public interface IUsuarioRepository
    {
        List<Usuario> GetUsuarios();
        Usuario? GetUsuarioById(int id);
        bool? validarAdmin();
        void Adicionar(Usuario usuario);
        void Remover(int id);
        void Atualizar(Usuario usuario);
    }
}
