using RoyalGames.Domains;
using RoyalGames.DTOs.JogoDto;
using RoyalGames.Interfaces;

namespace RoyalGames.Application.Services
{
    public class JogoService
    {


        private readonly IJogoRepository _repository;

        public JogoService(IJogoRepository repository)
        {
            _repository = repository;
        }



    }
}
