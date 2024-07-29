using BCrypt.Net;

namespace Reneee.Application.Utils
{
    public static class PasswordUtils
    {
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool Verify(string password, string hashedPasswrod)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPasswrod);
        }

    }
}
