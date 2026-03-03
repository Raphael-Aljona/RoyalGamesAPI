using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RoyalGames.Contexts;
using RoyalGames.Domains;
using RoyalGames.Interfaces;
using VHBurguer.Exceptions;

namespace RoyalGames.Repositories
{
    public class PlataformaRepository : IPlataformaRepository
    {
        private readonly RoyalGamesContext _context;

        public PlataformaRepository(RoyalGamesContext context)
        {
            _context = context;
        }

        public void AtualizarPlataforma(Plataforma plataforma)
        {
            Plataforma? plataformaBanco = _context.Plataforma.FirstOrDefault(plat => plat.PlataformaID == plataforma.PlataformaID);

            if (plataformaBanco == null) throw new DomainException("Plataforma não encontrada");

            plataformaBanco.Nome = plataforma.Nome;

            _context.SaveChanges();
        }
        public void CadastrarPlataforma(Plataforma plataforma)
        {
            _context.Add(plataforma);
            _context.SaveChanges();
        }

        public void DeletarPlataforma(int id)
        {
            Plataforma? plataforma = _context.Plataforma.FirstOrDefault(idPlat => idPlat.PlataformaID == id);

            if (plataforma == null) throw new DomainException("Plataforma não encontrada");

            _context.Remove(plataforma!);
            _context.SaveChanges();
        }

        public Plataforma GetPlataformaById(int id)
        {
            Plataforma? plataforma = _context.Plataforma.FirstOrDefault(idPlat => idPlat.PlataformaID == id);
            return plataforma!;
        }

        public List<Plataforma> GetPlataformas()
        {
            return _context.Plataforma.ToList();
        }

        public bool PlataformaExiste(string nome)
        {
            return _context.Plataforma.Any(plat => plat.Nome == nome);
        }
    }
}
