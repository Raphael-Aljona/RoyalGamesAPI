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
<<<<<<< HEAD
                Nome= jogo.Nome,
                Preco= jogo.Preco,
                Descricao= jogo.Descricao,
                StatusProduto=jogo.StatusJogo,
                CategoriaID = categoria.Categoria.Select(c => c.CategoriaID).ToList(),
=======
                Nome = jogo.Nome,
                Preco = jogo.Preco,
                Descricao = jogo.Descricao,
                StatusProduto = jogo.StatusJogo,
>>>>>>> a879481e181cc5ff596af711d9a1d3a07d0b0171

                Categoria = jogo.Categoria.Select(c => c.Nome).ToList(),

                UsuarioID = jogo.UsuarioID,
                UsuarioNome = jogo.Usuario.Nome,
                UsuarioEmail = jogo.Usuario.Email

            };
        }
    }
}
