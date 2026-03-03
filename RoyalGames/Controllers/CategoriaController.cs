using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoyalGames.Application.Services;
using RoyalGames.DTOs.CategoriaDto;
using RoyalGames.Exceptions;
using RoyalGames.Repositories;

namespace RoyalGames.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {

        private readonly CategoriaService _service;

        public CategoriaController(CategoriaService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<LerCategoriaDto>> Listar()
        {
            List<LerCategoriaDto> c = _service.Listar();
            return Ok(c);
        }

        [HttpPost("{id}")]

        public ActionResult<LerCategoriaDto> ObterPorId(int id)
        {
            LerCategoriaDto c = _service.ObterPorId(id);

            if (c == null)
            {
                return StatusCode(404);
            }

            return Ok(c);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Adicionar(CriarCategoriaDto criarDto)
        {
            try
            {
                _service.Adicionar(criarDto);
                return StatusCode(201);
            }
            catch (Exception)
            {

                return BadRequest(Exception.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]

        public ActionResult Atualizar(int id, CriarCategoriaDto criarDto)
        {
            try
            {
                _service.Atualizar(id, criarDto);
                return NoContent();
            }
            catch (DomainException)
            {

                return BadRequest(DomainException.Message);
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
            }
            catch (DomainException)
            {
                return BadRequest(DomainException.Message);
            }
        }
    }
}
