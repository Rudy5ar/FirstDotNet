using FirstDotNet.Context;
using FirstDotNet.Models;

namespace FirstDotNet.Repositories
{

    public interface IUserRepository
    {
        bool IsValidUser(string username, string password);

    }

    public class UserRepository : IUserRepository
    {

        private readonly SocialMediaContext _context;

        public UserRepository(SocialMediaContext socialMediaContext) {
            _context = socialMediaContext;
        }

        public bool IsValidUser(string username, string password)
        {
            User? user = _context.Users.FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                return false;
            }

            return BCrypt.Net.BCrypt.Verify(password, user.Password);
        }
    }
}
