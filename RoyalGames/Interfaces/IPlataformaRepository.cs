using RoyalGames.Domains;

namespace RoyalGames.Interfaces
{
    public interface IPlataformaRepository
    {
        public List<Plataforma> GetPlataformas();
        public Plataforma GetPlataformaById(int id);
        public void AtualizarPlataforma(Plataforma plataforma);
        public void DeletarPlataforma(int id);
        public void CadastrarPlataforma(Plataforma plataforma);
        public bool PlataformaExiste(string nome);
    }
}
