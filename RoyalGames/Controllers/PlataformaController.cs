using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoyalGames.Application.Services;
using RoyalGames.Domains;
using RoyalGames.DTOs.PlataformaDto;
using VHBurguer.Exceptions;

namespace RoyalGames.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlataformaController : ControllerBase
    {
        private readonly PlataformaService _service;

        public PlataformaController(PlataformaService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<LerPlataformaDto>> Listar()
        {
            List<LerPlataformaDto> p = _service.GetPlataformas();
            return Ok(p);
        }

        [HttpGet("{id}")]
        public ActionResult<LerPlataformaDto> ObterPorId(int id)
        {
            LerPlataformaDto p = _service.GetPlataformaById(id);
            if (p==null)
            {
                return NotFound();
            }

            return Ok(p);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Adicionar(CriarPlataformaDto criardto)
        {
            try
            {
                _service.Adicionar(criardto);

                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]

        public ActionResult Atualizar(LerPlataformaDto lerDto, int id)
        {
            try
            {
                _service.Atualizar(lerDto, id);
                return NoContent();
            }
            catch (DomainException ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]

        public ActionResult Remover(int id) {
            try
            {
                _service.Remover(id);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
