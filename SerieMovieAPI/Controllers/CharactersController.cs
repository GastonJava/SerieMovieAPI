using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SerieMovieAPI.Core.IConfiguration;
using SerieMovieAPI.Models;
using SerieMovieAPI.Models.DTOs.Characters;
using SerieMovieAPI.Models.DTOs.Movieseries;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerieMovieAPI.Controllers
{
    [Route("[controller]")]
    public class CharactersController : ControllerBase
    {
        private readonly ILogger<CharactersController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment env;
        private readonly IMapper _mapper;

        public CharactersController(
            ILogger<CharactersController> logger,
            IUnitOfWork unitOfWork,
            IWebHostEnvironment webHostEnvironment,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            this.env = webHostEnvironment;
            _mapper = mapper;
        }

        /// <summary>
        ///  Gets the list of all Characters by his ID. 
        /// </summary>
        /// <returns>The List of Characters by ID</returns>
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

        /// <summary>
        ///  Gets the list of all Characters by his name. 
        /// </summary>
        /// <returns>The List of Characters by his name</returns>
        //GET /characters?name=nombre
        [HttpGet("byname")]
        public async Task<ActionResult> GetbyName([FromQuery] string name) 
        {
            var getbyname = await _unitOfWork.Characters.GetbyNames(name);

            if (getbyname == null || !getbyname.Any()) 
            {
                return NotFound($"Character with name: {name}, doesn't exist in the current record");
            }

            var getbynameDTO = _mapper.Map<IEnumerable<CharactersDTO>>(getbyname);

            return Ok(getbynameDTO);
        }

        /// <summary>
        ///  Gets the list of all Characters by age. 
        /// </summary>
        /// <returns>The List of Characters by the Age</returns>
        //GET /characters? age = edad
        [HttpGet("byage")]
        public async Task<ActionResult> GetbyAge([FromQuery] int age)
        {
            var getbyage = await _unitOfWork.Characters.GetbyAges(age);

            if (getbyage == null || !getbyage.Any())
            {
                return NotFound($"Character with the age of {age},  doesn't exists in current record ");
            }

            var getageDTO = _mapper.Map<IEnumerable<CharactersDTO>>(getbyage);
            return Ok(getageDTO);
        }

        /// <summary>
        ///  Gets the list of all Characters by movieID. 
        /// </summary>
        /// <returns>The List of Characters by movieID</returns>
        // /characters? id = idmovie
        [HttpGet("bymovie")]
        public async Task<ActionResult> GetcharacByIdmovie([FromQuery] int idmovie) 
        {

           var resul  = await _unitOfWork.Characters.GetcharacterByIdmovie(idmovie);

            var characterdto = _mapper.Map<IEnumerable<CharactersDTO>>(resul);

            return Ok(characterdto);
        }

        /// <summary>
        ///  Gets the list of all Characters. 
        /// </summary>
        /// <returns>The List of Characters</returns>
        //GET: api/Character
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Todos()
        {
            var todos = await _unitOfWork.Characters.GetTodos();

            var select = todos.Select(c => new { c.Image_Characterbyte, c.Name_Character });

            _logger.LogInformation($"Traemos todos los characteres solo su imagen y nombre");
            return Ok(select);
        }

        /// <summary>
        /// Creates an Character.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     POST /CreateCharacter
        ///     {
        ///     
        ///       "File" : "image",
        ///       "Name_Character" : "Batman",
        ///       "Age_Character" : 32,
        ///       "Weight_Character" : 78,4,
        ///       "History_Character" : "Batman is an main character from Batman movie",
        ///       "title_movie" : "Batman, Batman Returns, Batman Forever",
        ///       
        ///     }
        /// </remarks>
        /// <param name="character"></param>
        /// <response code="201">Returns the newly created character</response>
        /// <response code="400">If the item is null</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateCharacter([FromForm] CharactersDTO character) 
        {

            if (ModelState.IsValid)
            {

                if (character.File != null)
                {
                    // Convertir imagen a byte y guardarlo en Image_Characterbyte (en base de datos)
                    character.Image_Characterbyte = _unitOfWork.Images.imageToBytearrayFromMemory(character.File);
                }

                //TODO: Separamos los titulos de las peliculas (",") y quitamos espacio en blanco
                var textsplit = character.Title_movie.Split(",")
                    .Select(x => x.Trim())
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .ToArray();

                List<MovieserieModel> peliculas = new();
                List<string> noexistepelicula = new();

                for (int i = 0; i < textsplit.Length; i++)
                {
                    var moviesexiste = await _unitOfWork.Movieseries.GetByString(textsplit[i]);

                    if (moviesexiste)
                    {
                        var moviess = await _unitOfWork.Movieseries.GetByTitle(textsplit[i]);
                        peliculas.Add(moviess.FirstOrDefault());
                    }
                    else
                    {
                        noexistepelicula.Add(textsplit[i]);
                    }
                }

                var moviesdtos = _mapper.Map<IEnumerable<MovieseriesDTO>>(peliculas);

                for (int i = 0; i < moviesdtos.Count(); i++)
                {
                    character.Movieseries.Add(moviesdtos.ElementAt(i));
                }
         
                var charactermodel = _mapper.Map<CharacterModel>(character);
                await _unitOfWork.Characters.Add(charactermodel);
                await _unitOfWork.CompleteAsync();

                return CreatedAtAction(nameof(GetItem), new { id = character.CharacterId }, character);
            }
            return BadRequest(ModelState);
        }


        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateCharacter(int id, CharactersDTO characterdto)
        {
            if (id != characterdto.CharacterId)
            {
                return BadRequest();
            }

            var charactermodel = _mapper.Map<CharacterModel>(characterdto);
            await _unitOfWork.Characters.Upsert(charactermodel);
            await _unitOfWork.CompleteAsync();

            /* return Ok(character); */
            return NoContent(); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacter(int id)
        {
            var character = await _unitOfWork.Characters.GetbyIdkey(id);

            if (character == null)
            {
                return BadRequest(); 
            }

            await _unitOfWork.Characters.Deletekey(id);
            await _unitOfWork.CompleteAsync();

            return Ok(character);
        }
    }
}