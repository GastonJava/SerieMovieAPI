using SerieMovieAPI.Core.IRepositories;
using System.Threading.Tasks;

namespace SerieMovieAPI.Core.IConfiguration
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }

        ICharacterRepository Characters { get; }

        IGenreRepository Genres { get; }

        IMovieserieRepository Movieseries { get; }
        
        IImageRepository Images {  get; }

        Task CompleteAsync();
    }
}
