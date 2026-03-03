using Microsoft.IdentityModel.Tokens;
using RoyalGames.Domains;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VHBurguer.Exceptions;

namespace RoyalGames.Application.Autenticacao
{
    public class GeradorTokenJwt
    {
        private readonly IConfiguration _config;

        public GeradorTokenJwt(IConfiguration config)
        {
            _config = config;
        }

        public string GerarToken(Usuario usuario)
        {
            var key = _config["Jwt:Key"]!;

            var issuer = _config["Jwt:Issuer"]!;

            var audience = _config["Jwt:Audience"]!;

            var expiraEmMinutos = int.Parse(_config["Jwt:ExpiraEmMinutos"]!);

            var keyBytes = Encoding.UTF8.GetBytes(key);

            if (keyBytes.Length < 32) throw new DomainException("Token inválido");

            var securityKey = new SymmetricSecurityKey(keyBytes);

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.UsuarioID.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim(ClaimTypes.Email, usuario.Email),
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(expiraEmMinutos),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
