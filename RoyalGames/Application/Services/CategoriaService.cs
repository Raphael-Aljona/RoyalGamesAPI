using RoyalGames.Domains;
using RoyalGames.DTOs.CategoriaDto;
using RoyalGames.Interfaces;
using VHBurguer.Exceptions;

namespace RoyalGames.Application.Services
{
    public class CategoriaService
    {

        private readonly ICategoriaRepository _repository;

        public CategoriaService(ICategoriaRepository repository)
        {
            _repository = repository;
        }

        public List<LerCategoriaDto> Listar()
        {
            List<Categoria> categorias = _repository.Listar();

            List<LerCategoriaDto> categoriaDto = categorias.Select(c => new LerCategoriaDto
            {
                CategoriaID = c.CategoriaID,
                Nome = c.Nome
            }).ToList();

            return categoriaDto;
        }

        public LerCategoriaDto ObterPorId(int id)
        {
            Categoria categoria = _repository.ObterPorId(id);

            if (categoria == null)
            {
                throw new DomainException("Genero/categoria não encontrado");
            }

            LerCategoriaDto categoriaDto = new LerCategoriaDto
            {
                CategoriaID = categoria.CategoriaID,
                Nome = categoria.Nome
            };

            return categoriaDto;

        }

        private static void ValidarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new DomainException("Nome é obrigatório");
            }
        }

        public void Adicionar(CriarCategoriaDto criarDto)
        {
            ValidarNome(criarDto.Nome);

            if (_repository.NomeExiste(criarDto.Nome))
            {
                throw new DomainException("Categoria já existente");
            }

            Categoria categoria = new Categoria
            {
                Nome = criarDto.Nome,
            };

            _repository.Adicionar(categoria);
        }

        public void Atualizar(int id, CriarCategoriaDto criarDto)
        {
            ValidarNome(criarDto.Nome);
            Categoria categoriaBanco = _repository.ObterPorId(id);

            if (categoriaBanco == null)
            {
                throw new DomainException("Categoria/Genero não encontrada.");
            }

            if (_repository.NomeExiste(criarDto.Nome, categoriaIdAtual: id))
            {
                throw new DomainException("já existe outra categoria/gênero com esse nome.");
            }

            categoriaBanco.Nome = criarDto.Nome;

            _repository.Atualizar(categoriaBanco);

        }

        public void Remover(int id)
        {
            Categoria categoriaBanco = _repository.ObterPorId(id);

            if (categoriaBanco == null)
            {
                throw new DomainException("Categoria/gênero não encontrada");

            }
            _repository.Remover(id);
        }


    }
}
