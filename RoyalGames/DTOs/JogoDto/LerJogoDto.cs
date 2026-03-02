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
        public List<int> ClassificacaoIndicativaIds { get; set; } = new();
        public List<string> ClassificacaoIndicativa { get; set; } = new();

        //usuario que cadastrou
        public int? UsuarioID { get; set; }
        public string? UsuarioNome { get; set; }
        public string? UsuarioEmail { get; set; }
    }
}
