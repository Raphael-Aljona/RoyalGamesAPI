namespace RoyalGames.DTOs.JogoDto
{
    public class AtualizarJogoDto
    {
        public string Nome { get; set; } = null!;
        public decimal Preco { get; set; }
        public string Descricao { get; set; } = null!;

        public IFormFile Imagem { get; set; } = null!;

        public List<int> ClassificacaoIndicativaIds { get; set; } = new();

        public List<int> Categoria {  get; set; } = new();

        public List<int> Plataforma { get; set; } = new();

        public bool? StatusJogo { get; set; }

        public bool? AdminUsuario {  get; set; }
    }
}
