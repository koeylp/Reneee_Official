using System.ComponentModel.DataAnnotations;

namespace Reneee.Application.Models.Identity
{
    public class RegisterRequest
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        [Required]
        public string Gender { get; set; }
        public string Dob { get; set; }
    }
}
