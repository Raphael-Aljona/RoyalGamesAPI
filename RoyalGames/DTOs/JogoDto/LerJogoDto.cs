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
        public int? ClassificacaoIndicativa { get; set; } = new();
        public List<int> CategoriaIds { get; set; } = new();
        public List<string> Categoria { get; set; } = new();
        public List<int> PlataformaIds { get; set; } = new();
        public List<string> Plataforma { get; set; } = new();

        //usuario que cadastrou
        public int? UsuarioID { get; set; }
        public string? UsuarioNome { get; set; }
        public string? UsuarioEmail { get; set; }
    }
}
