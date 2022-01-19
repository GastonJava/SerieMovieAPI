using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SerieMovieAPI.Core.IConfiguration;
using SerieMovieAPI.Models;
using System.Threading.Tasks;

namespace SerieMovieAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GenresController : Controller
    {
        private readonly ILogger<GenresController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public GenresController(
            ILogger<GenresController> logger,
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetItem(int id)
        {

            var character = await _unitOfWork.Characters.GetbyIdkey(id);

            if (character == null)
            {
                return NotFound();
            }
            return Ok(character);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGenre(GenreModel genre)
        {
            if (ModelState.IsValid)
            {
                genre.GenreId = genre.GenreId;
                await _unitOfWork.Genres.Add(genre);
                await _unitOfWork.CompleteAsync();

                return CreatedAtAction("GetItem", new { genre.GenreId }, genre);
            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var genres = await _unitOfWork.Genres.All();
            return Ok(genres);
        }
    }
}