using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SerieMovieAPI.Core.IConfiguration;
using SerieMovieAPI.Core.IRepositories;
using SerieMovieAPI.Services;
using System.Threading.Tasks;

namespace SerieMovieAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArchivosController : ControllerBase
    {
        private readonly IStorageService _storageService;

        private readonly IUnitOfWork _unitOfWork;

        public ArchivosController(IStorageService storageService)
        {
            _storageService = storageService;
        }

        [HttpGet]
        public IActionResult Get() 
        {
            return Ok("Corriendo el archivo");
        }

        [HttpPost]
        public async Task<IActionResult> Subir(IFormFile formfile) 
        {

            _storageService.Subir(formfile);
            return Ok();
        }
    }
}
