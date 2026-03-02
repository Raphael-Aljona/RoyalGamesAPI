using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoyalGames.Application.Services;
using System.Security.Claims;

namespace RoyalGames.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JogoController : ControllerBase
    {
        private readonly JogoService _service;

        public JogoController(JogoService service)
        {
            _service = service;
        }

        private int ObterUsuarioLogado()
        {
            string? idtexto = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(idtexto))
            {
                throw new Exception("Usuário não autenticado");
            }
            return int.Parse(idtexto);
        }
    }
    
}