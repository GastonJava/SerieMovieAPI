using SerieMovieAPI.Models;
using SerieMovieAPI.Models.DTOs.Characters;
using System.Threading.Tasks;

namespace SerieMovieAPI.Core.IRepositories
{
    public interface ICharacterRepository : IGenericRepository<CharacterModel>
    {
        /* public virtual Task<bool> AddDTO(CharactersDTO character); */
        
    }
}
