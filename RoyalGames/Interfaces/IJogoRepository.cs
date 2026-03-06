using Microsoft.AspNetCore.Components.Web;
using RoyalGames.Domains;

namespace RoyalGames.Interfaces
{
    public interface IJogoRepository
    {
        List<Jogo> Listar();

        Jogo ObterPorId(int id);

        byte[] ObterImagem(int id);
        bool NomeExiste(string nome, int? JogoIdAtual = null);
        void Adicionar(Jogo jogo, List<int> categoriaIds, List<int> plataformaIds);
        void Atualizar(Jogo jogo, List<int> categoriaIds, List<int> plataformaIds);
        void Remover(int id);

    }
}
