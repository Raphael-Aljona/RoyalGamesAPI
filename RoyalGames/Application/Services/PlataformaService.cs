using RoyalGames.Domains;
using RoyalGames.DTOs.PlataformaDto;
using RoyalGames.DTOs.UsuarioDto;
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

        public LerPlataformaDto Adicionar(LerPlataformaDto lerDto)
        {
            ValidarNome(lerDto.Nome);

            if (_repository.PlataformaExiste(lerDto.Nome)) throw new DomainException("Essa plataforma já existe");

            Plataforma plataforma = new Plataforma
            {
                Nome = lerDto.Nome,
            };

            _repository.CadastrarPlataforma(plataforma);
            return LerDto(plataforma);
        }

        public LerPlataformaDto Atualizar(LerPlataformaDto lerDto, int id)
        {
            Plataforma plataforma = _repository.GetPlataformaById(id);

            ValidarNome(lerDto.Nome);

            if (plataforma == null) throw new DomainException("Plataforma não encontrada");

            Plataforma? plataformaComMemsoNome = _repository.GetPlataformas().FirstOrDefault(plat => plat.Nome == lerDto.Nome);

            if (plataformaComMemsoNome != null && plataformaComMemsoNome.PlataformaID != id) throw new DomainException("Já existe uma plataforma com este nome");

            plataforma.Nome = lerDto.Nome;

            _repository.AtualizarPlataforma(plataforma);
            return LerDto(plataforma);
        }

        public void Remover(int id)
        {
            Plataforma plataforma = _repository.GetPlataformaById(id);

            if (plataforma == null) throw new DomainException("Essa plataforma não existe");

            _repository.DeletarPlataforma(id);
        }


    }
}