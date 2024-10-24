using FirstDotNet.Context;
using FirstDotNet.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstDotNet.Repositories
{

    public interface IUserRepository {
        Task<User> addUser(User newUser);
        Task<bool> emailExists(string email);
        Task<User?> getUserByUsername(string username);
        Task<bool> IsValidUser(string username, string password);
        Task<bool> userExists(string username);
    }

    public class UserRepository : IUserRepository {

        private readonly SocialMediaContext _context;

        public UserRepository(SocialMediaContext socialMediaContext) {
            _context = socialMediaContext;
        }

        public Task<User> addUser(User newUser) {
            return Task.Run(() => {
                _context.Users.Add(newUser);
                _context.SaveChanges();
                return newUser;
            });
        }

        public Task<bool> emailExists(string email) {
            return _context.Users.AnyAsync(u => u.Email == email);
        }

        public Task<bool> userExists(string username) {
            return _context.Users.AnyAsync(u => u.Username == username);
        }

        public async Task<bool> IsValidUser(string username, string password) {
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user == null) {
                return false;
            }

            return BCrypt.Net.BCrypt.Verify(password, user.Password);
        }

        public Task<User?> getUserByUsername(string username) {
            return _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}
