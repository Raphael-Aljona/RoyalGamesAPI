using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoyalGames.Application.Services;
using RoyalGames.DTOs.JogoDto;
using System.Security.Claims;
using VHBurguer.Exceptions;

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

        [HttpGet]
        public ActionResult<List<LerJogoDto>> Listar()
        {
            List<LerJogoDto> jogo = _service.Listar();
            return Ok(jogo);
        }

        [HttpGet("{id}")]
        public ActionResult<LerJogoDto> ObterPorId(int id)
        {
            LerJogoDto j = _service.ObterPorId(id);

            if (j == null)
            {
                return NotFound();
            }

            return Ok(j);
        }

        [HttpGet("{id}/imagem")]

        public ActionResult ObterImagem(int id)
        {
            try
            {
                var imagem = _service.ObterImagem(id);
                return File(imagem, "imagem/jpeg");
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [Authorize]

        public ActionResult Adicionar([FromForm] CriarJogoDto jogoDto)
        {

            try
            {
                int usuarioId = ObterUsuarioLogado();
                _service.Adicionar(jogoDto, usuarioId);
                return StatusCode(201);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]

        [Consumes("multipart/form-data")]
        [Authorize]

        public ActionResult Atualizar(int id, [FromForm] AtualizarJogoDto jogoDto)
        {
            try
            {
                _service.Atualizar(id, jogoDto);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult Remover(int id)
        {
            try
            {
                _service.Remover(id);
                return NoContent();
            }catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}