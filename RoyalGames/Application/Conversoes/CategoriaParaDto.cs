using RoyalGames.Domains;
using RoyalGames.DTOs.JogoDto;

namespace RoyalGames.Application.Conversoes
{
    public class CategoriaParaDto
    {
        public static LerJogoDto ConverterparaDto(Jogo jogo, Categoria categoria)
        {
            return new LerJogoDto
            {
                JogoID = jogo.JogoID,
                Nome= jogo.Nome,
                Preco= jogo.Preco,
                Descricao= jogo.Descricao,
                StatusProduto=jogo.StatusJogo,
                CategoriaID = categoria.Categoria.Select(c => c.CategoriaID).ToList(),

                Categoria = jogo.Categoria.Select(c => c.Nome).ToList(),

                UsuarioID = jogo.UsuarioID,
                UsuarioNome = jogo.Usuario.Nome,
                UsuarioEmail = jogo.Usuario.Email

            }
        }
    }
}
