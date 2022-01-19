using System.ComponentModel.DataAnnotations;

namespace SerieMovieAPI.Models.DTOs.Requests
{
    public class RegistroUsuarioDto
    {
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
