using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SerieMovieAPI.Core.IConfiguration;
using SerieMovieAPI.Models;
using System;
using System.Threading.Tasks;

namespace SerieMovieAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {

        private readonly ILogger<UsersController> _logger;

        private readonly IUnitOfWork _unitOfWork;

        public UsersController(
            ILogger<UsersController> logger,
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;

        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetItem(Guid id)
        {

            var user = await _unitOfWork.Users.GetbyId(id);

            if (user == null)
            {

                return NotFound();
            }

            return Ok(user);

        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            if (ModelState.IsValid)
            {
                user.Id = Guid.NewGuid();
                await _unitOfWork.Users.Add(user);
                await _unitOfWork.CompleteAsync();

                return CreatedAtAction("GetItem", new { user.Id }, user);
            }

            return BadRequest(ModelState);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _unitOfWork.Users.All();
            return Ok(users);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }
            await _unitOfWork.Users.Upsert(user);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _unitOfWork.Users.GetbyId(id);

            if (user == null) 
            {
                return BadRequest();
            }

            await _unitOfWork.Users.Delete(id);
            await _unitOfWork.CompleteAsync();

            return Ok(user);
        }


    }

}