using RoyalGames.Domains;

namespace RoyalGames.DTOs.JogoDto
{
    public class CriarJogoDto
    {
        public string Nome { get; set; } = null!;
        public decimal Preco { get; set; }
        public string Descricao { get; set; } = null!;

        public IFormFile Imagem { get; set; } = null!;

        public List<int> UsuarioIds { get; set; } = new();

        public List<int> CategoriaIds { get; set; } = new();
        public List<int> PlataformaIds { get; set; } = new();
        public int ClassificacaoIndicativaIds { get; set; } = new();
        public bool AdminUsuario {  get; set; }
    }
}
