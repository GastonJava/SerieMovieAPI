using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SerieMovieAPI.Core.IConfiguration;
using SerieMovieAPI.Models;
using SerieMovieAPI.Models.DTOs.Movieseries;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerieMovieAPI.Controllers
{
    public class MovieserieController : ControllerBase
    {
        private readonly ILogger<MovieserieController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MovieserieController(
            ILogger<MovieserieController> logger,
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        ///  Gets the list of all Movies/Series by ID. 
        /// </summary>
        /// <returns>The List of Movies/Series by ID</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult> GetItem(int id)
        {
            var movieserie = await _unitOfWork.Movieseries.GetbyIdkey(id);
            if (movieserie == null)
            {
                return NotFound();
            }
            return Ok(movieserie);
        }

        /// <summary>
        /// Creates an Movie/Serie.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     POST /CreateMovieserie
        ///     {
        ///     
        ///       "Title_Movserie" : "Batman",
        ///       "moviefile" : "Byte array",
        ///       "Date_Movserie" : 1900-11-11,
        ///       "Rating_Movserie" : 5,
        ///       
        ///     }
        /// </remarks>
        /// <param name="movieseriedto"></param>
        /// <response code="201">Returns the newly created Movie/Serie</response>
        /// <response code="400">If the item is null</response>
        [HttpPost("CreateMovie")]
        public async Task<IActionResult> CreateMovieserie([FromForm]MovieseriesDTO movieseriedto) 
        {
            if (ModelState.IsValid) 
            {
                movieseriedto.MovieserieId = movieseriedto.MovieserieId;

                var existetitulo = await _unitOfWork.Movieseries.GetByString(movieseriedto.Title_Movserie);

                if (existetitulo) {
                    _logger.LogInformation($"{movieseriedto.Title_Movserie}: ya existe en la base de datos");
                    return BadRequest("Ya existe ese titulo de pelicula");
                }

                //guardar imagen
                movieseriedto.Image_Movserie = _unitOfWork.Images.imageToBytearrayFromMemory(movieseriedto.moviefile);

                if (movieseriedto.Image_Movserie != null) {

                    var movieModel = _mapper.Map<MovieserieModel>(movieseriedto);

                    await _unitOfWork.Movieseries.Add(movieModel);
                    await _unitOfWork.CompleteAsync();
                }

                return CreatedAtAction(nameof(GetItem), new { id = movieseriedto.MovieserieId }, movieseriedto);
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        ///  Gets the list of all Movie/Serie. 
        /// </summary>
        /// <returns>The List of Movie/serie</returns>
        [HttpGet("GetMovies")]
        public async Task<IActionResult> Todos()
        {

            var todos = await _unitOfWork.Movieseries.GetTodos();
            var select = todos.Select(c => new { c.Title_Movserie, c.Image_Movserie, c.Date_Movserie });

            _logger.LogInformation($"Traemos todas las peliculas solo su imagen y nombre");
            return Ok(select);
        }


        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateMovieserie(int id, MovieserieModel movieserie)
        {
            if (id != movieserie.MovieserieId)
            {
                return BadRequest();
            }
            await _unitOfWork.Movieseries.Upsert(movieserie);
            await _unitOfWork.CompleteAsync();

            /* return Ok(character); */
            return NoContent();
        }
    }
}
