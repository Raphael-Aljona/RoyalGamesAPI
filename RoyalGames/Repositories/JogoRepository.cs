using Azure.Core;
using Microsoft.EntityFrameworkCore;
using RoyalGames.Contexts;
using RoyalGames.Domains;
using RoyalGames.Interfaces;

namespace RoyalGames.Repositories
{
    public class JogoRepository : IJogoRepository
    {
        private readonly RoyalGamesContext _context;

        public JogoRepository(RoyalGamesContext context)
        {
            _context = context;

        }

        public List<Jogo> Listar()
        {
            List<Jogo> jogos = _context.Jogo.Include(j => j.Plataforma).Include(j => j.Categoria
            ).Include(j => j.ClassificacaoIndicativa).Include(j => j.Usuario).ToList();
            return jogos;
        }

        public Jogo ObterPorId(int id)
        {
            Jogo? jogo = _context.Jogo.Include(jDb => jDb.Categoria).Include(jDb => jDb.Plataforma).Include(jDb => jDb.ClassificacaoIndicativa).Include(jDb => jDb.Usuario).FirstOrDefault(jDb => jDb.JogoID == id);
            return jogo;
        }

        public bool NomeExiste(string nome, int? jogoidAtual = null)
        {
            var jogoConsultado=_context.Jogo.AsQueryable();

            if (jogoidAtual.HasValue)
            {
                jogoConsultado = jogoConsultado.Where(j => j.JogoID != jogoidAtual.Value);
            }

            return jogoConsultado.Any(j=>j.Nome==nome);
        }

        public byte[]ObterImagem(int id)
        {
            var jogo=_context.Jogo.Where(j=>j.JogoID==id).Select(j=>j.Imagem).FirstOrDefault();
            return jogo;
        }

        public void Adicionar(Jogo jogo, List<int> categoriaIds, List<int> plataformaIds)
        {
            List<Categoria> c=_context.Categoria.Where(c=>categoriaIds.Contains(c.CategoriaID)).ToList();
            List<Plataforma> p = _context.Plataforma.Where(p => plataformaIds.Contains(p.PlataformaID)).ToList();
            jogo.Categoria = c;
            jogo.Plataforma = p;


            _context.Jogo.Add(jogo);
            _context.SaveChanges();
        }

        public void Atualizar(Jogo jogo, List<int> categoriaIds, List<int> plataformaIds)
        {
            Jogo? jogoBanco = _context.Jogo.Include(j => j.Categoria).FirstOrDefault(jAux => jAux.JogoID == jogo.JogoID);

            if (jogoBanco==null)
            {
                return;
            }

            jogoBanco.Nome = jogo.Nome;
            jogoBanco.Preco = jogo.Preco;
            jogoBanco.Descricao = jogo.Descricao;
            jogoBanco.ClassificacaoIndicativa=jogo.ClassificacaoIndicativa;

            if (jogo.Imagem != null && jogo.Imagem.Length > 0)
            {
                jogoBanco.Imagem = jogo.Imagem;
            }

            if (jogo.StatusJogo.HasValue)
            {
                jogoBanco.StatusJogo = jogo.StatusJogo;
            }


            var p = _context.Plataforma
                .Where(p => plataformaIds.Contains(p.PlataformaID))
                .ToList();

            jogoBanco.Plataforma.Clear();

            foreach (var plataforma in p)
            {
                jogoBanco.Plataforma.Add(plataforma);
            }
            _context.SaveChanges();

        }

        public void Remover(int id)
        {
            Jogo? j = _context.Jogo.FirstOrDefault(j => j.JogoID == id);

            if (j == null)
            {
                return;
            }

            _context.Jogo.Remove(j);
            _context.SaveChanges();
        }
    }
}
