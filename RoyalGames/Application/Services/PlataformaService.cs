using RoyalGames.Domains;
using RoyalGames.DTOs.PlataformaDto;
using RoyalGames.Interfaces;
using VHBurguer.Exceptions;

namespace RoyalGames.Application.Services
{
    public class PlataformaService
    {
        private readonly IPlataformaRepository _repository;

        public PlataformaService(IPlataformaRepository repository)
        {
            _repository = repository;
        }

        public void ValidarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new Exception("Nome inválido");
            }
        }

        public LerPlataformaDto LerDto(Plataforma plataforma)
        {
            LerPlataformaDto lerPlataformaDto = new LerPlataformaDto
            {
                Nome = plataforma.Nome,
            };

            return lerPlataformaDto;
        }

        public List<LerPlataformaDto> GetPlataformas()
        {
            List<Plataforma> plataformas = _repository.GetPlataformas();
            List<LerPlataformaDto> lerPlataformaDtos = plataformas.Select(plataforma => LerDto(plataforma)).ToList();

            return lerPlataformaDtos;
        }

        public LerPlataformaDto GetPlataformaById(int id)
        {
            Plataforma plataforma = _repository.GetPlataformaById(id);

            if (plataforma == null) throw new DomainException("Nenhuma plataforma encontrada");

            LerPlataformaDto lerPlatDto = LerDto(plataforma);
            return lerPlatDto;
        }


    }
}
