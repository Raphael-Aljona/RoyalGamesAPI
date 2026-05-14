namespace RoyalGames.DTOs.JogoDto
{
    public class LerJogoDto
    {
        public int JogoID { get; set; }

        public string Nome { get; set; } = null!;

        public decimal Preco { get; set; }

        public string Descricao { get; set; } = null!;

        public bool? StatusProduto { get; set; }

        // categorias 
        public string ClassificacaoIndicativa { get; set; } = null!;
        public List<int> CategoriaIds { get; set; } = new();
        public List<string> Categorias { get; set; } = new();
        public List<int> PlataformaIds { get; set; } = new();
        public List<string> Plataformas { get; set; } = new();

        //usuario que cadastrou
        public int? UsuarioID { get; set; }
        public string? UsuarioNome { get; set; }
        public string? UsuarioEmail { get; set; }
    }
}
