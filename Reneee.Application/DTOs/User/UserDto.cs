namespace Reneee.Application.DTOs.User
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Role { get; set; }
        public DateOnly Dob { get; set; }
        public int Status { get; set; }
    }
}
