using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RoyalGames.Application.Services;
using RoyalGames.DTOs.UsuarioDto;
using VHBurguer.Exceptions;

namespace RoyalGames.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _service;

        public UsuarioController(UsuarioService service) {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<LerUsuarioDto>> GetUsuarios()
        {
            List<LerUsuarioDto> usuarios = _service.GetUsuario();

            return StatusCode(200, usuarios);
        }

        [HttpGet("{id}")]
        public ActionResult<LerUsuarioDto> GetUsuarioById(int id)
        {
            LerUsuarioDto usuarioDto = _service.GetUsuarioById(id);

            return StatusCode(200, usuarioDto);
        }

        [HttpPost]
        public ActionResult<LerUsuarioDto> Adicionar(CriarUsuarioDto criarUsuarioDto)
        {
            try
            {
                LerUsuarioDto usuario = _service.Adicionar(criarUsuarioDto);
                if (usuario == null) return BadRequest();

                return StatusCode(201, usuario);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<LerUsuarioDto> Atualizar(int id, CriarUsuarioDto criar)
        {
            try
            {
                LerUsuarioDto usuarioDto = _service.Atualizar(criar, id);
                return StatusCode(200, usuarioDto);
            } catch (DomainException ex)
            {
                return BadRequest(ex.Message);  
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Remover(int id)
        {
            try
            {
                _service.Remover(id);

                return StatusCode(204);
            }
            catch (DomainException ex){ 
                return BadRequest(ex.Message);
            }
        }

    }
}
