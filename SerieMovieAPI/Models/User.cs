using System;
using System.ComponentModel.DataAnnotations;

namespace SerieMovieAPI.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Please enter Email")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        public string Email { get; set; }
        

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
