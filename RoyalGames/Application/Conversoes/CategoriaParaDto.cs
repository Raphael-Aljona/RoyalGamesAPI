using RoyalGames.Domains;
using RoyalGames.DTOs.JogoDto;

namespace RoyalGames.Application.Conversoes
{
    public class CategoriaParaDto
    {
        public static LerJogoDto ConverterparaDto(Jogo jogo)
        {
            return new LerJogoDto
            {
                JogoID = jogo.JogoID,
                Nome= jogo.Nome,
                Preco= jogo.Preco,
                Descricao= jogo.Descricao,
                StatusProduto=jogo.StatusJogo,


            }
        }
    }
}
