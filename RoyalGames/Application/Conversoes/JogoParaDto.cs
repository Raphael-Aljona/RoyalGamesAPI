using RoyalGames.Domains;
using RoyalGames.DTOs.JogoDto;

namespace RoyalGames.Application.Conversoes
{
    public class JogoParaDto
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

                CategoriaIds = jogo.Categoria.Select(c=>c.CategoriaID).ToList(),

                Categorias= jogo.Categoria.Select(c=>c.Nome).ToList(),

                PlataformaIds = jogo.Plataforma.Select(p=>p.PlataformaID).ToList(),

                Plataformas =jogo.Plataforma.Select(p=>p.Nome).ToList(),

                ClassificacaoIndicativa = jogo.ClassificacaoIndicativaID,

                UsuarioID = jogo.UsuarioID,
                UsuarioNome = jogo.Usuario.Nome,
                UsuarioEmail = jogo.Usuario.Email

            };
        }
    }
}
