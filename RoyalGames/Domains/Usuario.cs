using System;
using System.Collections.Generic;

namespace RoyalGames.Domains;

public partial class Usuario
{
    public int UsuarioID { get; set; }

    public string Nome { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool? StatusUsuario { get; set; }

    public bool? AdminUsuario { get; set; }

    public byte[] Senha { get; set; } = null!;

    public virtual ICollection<Jogo> Jogo { get; set; } = new List<Jogo>();
}
