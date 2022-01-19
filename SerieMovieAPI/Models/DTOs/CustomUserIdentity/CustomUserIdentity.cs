using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SerieMovieAPI.Models.DTOs.User
{
    public class CustomUserIdentity : IdentityUser
    {
        public string Name { get; set; }

        public string Lastname { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please Insert Password")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Password is not valid.")]
        public string Password { get; set; }
    }
}
