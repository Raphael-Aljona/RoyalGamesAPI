using Microsoft.Identity.Client;
using RoyalGames.Application.Conversoes;
using RoyalGames.Domains;
using RoyalGames.DTOs.JogoDto;
using RoyalGames.Interfaces;
using System.Reflection;
using VHBurguer.Exceptions;

namespace RoyalGames.Application.Services
{
    public class JogoService
    {


        private readonly IJogoRepository _repository;

        public JogoService(IJogoRepository repository)
        {
            _repository = repository;
        }

        public List<LerJogoDto> Listar()
        {
            List<Jogo> jogos = _repository.Listar();

            List<LerJogoDto> jogosDto = jogos.Select(JogoParaDto.ConverterparaDto).ToList();
            return jogosDto;
        }

        public LerJogoDto ObterPorId(int id)
        {
            Jogo jogo = _repository.ObterPorId(id);

            if (jogo == null)
            {
                throw new DomainException("Jogo não encontrado");
            }

            return JogoParaDto.ConverterparaDto(jogo);
        }

        private static void ValidarCadastro(CriarJogoDto jogoDto)
        {
            if (string.IsNullOrWhiteSpace(jogoDto.Nome))
            {
                throw new DomainException("Nome é obrigatório");
            }

            if (jogoDto.Preco < 0)
            {
                throw new DomainException("Preço deve ser maior que zero");
            }

            if (string.IsNullOrWhiteSpace(jogoDto.Descricao))
            {
                throw new DomainException("Descrição é obrigatório");
            }

            if (jogoDto.Imagem == null || jogoDto.Imagem.Length == 0)
            {
                throw new DomainException("Imagem é obrigatória");
            }

            if (jogoDto.CategoriaIds == null || jogoDto.CategoriaIds.Count == 0)
            {
                throw new DomainException("Produto deve ter ao menos uma categoria");
            }
            if (jogoDto.PlataformaIds == null || jogoDto.PlataformaIds.Count == 0)
            {
                throw new DomainException("Produto deve ter ao menos uma plataforma");
            }
            if (jogoDto.Imagem == null || jogoDto.Imagem.Length == 0)
            {
                throw new DomainException("Produto deve ter ao menos uma plataforma");
            }

        }

        public byte[] ObterImagem(int id)
        {
            byte[] imagem = _repository.ObterImagem(id);

            if (imagem == null || imagem.Length == 0)
            {
                throw new DomainException("imagem não encontrada");
            }

            return imagem;
        }

        public LerJogoDto Adicionar(CriarJogoDto jogoDto, int usuarioId)
        {
            ValidarCadastro(jogoDto);

            if (_repository.NomeExiste(jogoDto.Nome))
            {
                throw new DomainException("Jogo já existente");
            }

            Jogo jogo = new Jogo
            {
                Nome = jogoDto.Nome,
                Preco = jogoDto.Preco,
                Descricao = jogoDto.Descricao,
                Imagem = ImagemParaBytes.ConverterImagem(jogoDto.Imagem),
                StatusJogo = true,
                UsuarioID = usuarioId,
            };

            _repository.Adicionar(jogo, jogoDto.CategoriaIds, jogoDto.PlataformaIds);
            return JogoParaDto.ConverterparaDto(jogo);

        }

        public LerJogoDto Atualizar(int id, AtualizarJogoDto jogoDto)
        {
            Jogo jogoBanco = _repository.ObterPorId(id);
            if (jogoBanco == null)
            {

                throw new DomainException("Jogo não encontrado");
            }

            if (_repository.NomeExiste(jogoDto.Nome, JogoIdAtual: id))
            {
                throw new DomainException("Já existe outro jogo com esse nome");
            }

            if (jogoDto.Preco < 0)
            {
                throw new DomainException("Preco deve ser maior que zero.");
            }

            jogoBanco.Nome = jogoDto.Nome;
            jogoBanco.Preco = jogoDto.Preco;
            jogoBanco.Descricao = jogoDto.Descricao;

            if (jogoDto.Imagem != null && jogoDto.Imagem.Length > 0)
            {
                jogoBanco.Imagem = ImagemParaBytes.ConverterImagem(jogoDto.Imagem);
            }

            if (jogoDto.StatusJogo.HasValue)
            {
                jogoBanco.StatusJogo = jogoDto.StatusJogo.Value;
            }

            _repository.Atualizar(jogoBanco, jogoDto.Categoria, jogoDto.Plataforma);
            return JogoParaDto.ConverterparaDto(jogoBanco);
        }
         public void Remover(int id)
        {
            Jogo jogo=_repository.ObterPorId(id);

            if (jogo==null)
            {
                throw new DomainException("Jogo não encontrado");
            }

            _repository.Remover(id);
        }
    }
}
