using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace SerieMovieAPI.Services
{
    public interface IStorageService
    {
        void Subir(IFormFile formFile);
    }
}
