using System.ComponentModel.DataAnnotations;

namespace Reneee.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public Role Role { get; set; }
        [Required]
        public DateOnly Dob { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Status { get; set; }
    }
}
