using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SerieMovieAPI.Configuration;
using SerieMovieAPI.Models;
using SerieMovieAPI.Models.DTOs.Requests;
using SerieMovieAPI.Models.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SerieMovieAPI.Controllers
{
    /* [ApiController] */
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly UserManager<User> _userManager;
        private readonly JwtConfig _jwtConfig;

        public AuthController(
            UserManager<User> userManager,
            IOptionsMonitor<JwtConfig> optionsMonitor)
        {
            _userManager = userManager;
            _jwtConfig = optionsMonitor.CurrentValue;
        }


        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegistroUsuarioDto user)
        {
            if (ModelState.IsValid)
            {
                // verficamos el si el email del usuario existe en la base de datos
                var existingUser = await _userManager.FindByEmailAsync(user.Email);

                if (existingUser != null)
                {
                    // si existe el mail en la base de datos
                    return BadRequest(new RegistrationResponse()
                    {
                        Errors = new List<string>() {
                          "Email ya se encuentra registrado"
                        },
                        Success = false
                    });
                }

                // creamos el usuario
                var newUser = new User
                {
                    Email = user.Email,
                    FirstName = user.Username
                };
                var isCreated = await _userManager.CreateAsync(newUser, user.Password);

                // Si el nuevo Usuario es creado
                if (isCreated.Succeeded)
                {
                    // creamos un nuevo token de usuario
                    var jwtToken = GenerateJwtToken(newUser);

                    return Ok(new RegistrationResponse()
                    {
                        Success = true,
                        Token = jwtToken
                    });
                }
                else
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Errors = isCreated.Errors.Select(e => e.Description).ToList(),
                        Success = false
                    });
                }
            }
            return BadRequest(new RegistrationResponse()
            {
                Errors = new List<string>() {
                    "Invalid payload"
                },
                Success = false
            });
        }

        private string GenerateJwtToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new
                SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }
    }
}
