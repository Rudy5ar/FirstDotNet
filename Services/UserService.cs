using FirstDotNet.Models;
using FirstDotNet.Repositories;

namespace FirstDotNet.Services
{

    public interface IUserService
    {
        bool IsValidUser(string username, string password);
        Task<User> loginUser(LoginRequest loginRequest);
        Task<User> registerUser(RegisterRequest request);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> loginUser(LoginRequest loginRequest) {
            if (await _userRepository.IsValidUser(loginRequest.username, loginRequest.password)) {
                return await _userRepository.getUserByUsername(loginRequest.username);
            }
            throw new UnauthorizedAccessException("Invalid username or password");
        }

        public async Task<User> registerUser(RegisterRequest request) {
            
            if(await _userRepository.userExists(request.username)) {
                throw new ArgumentException("Username already exists");
            }

            if (await _userRepository.emailExists(request.email)) {
                throw new ArgumentException("Email already exists");
            }

            var newUser = new User {
                Username = request.username,
                Email = request.email,
                Password = BCrypt.Net.BCrypt.HashPassword(request.password)
            };

            return await _userRepository.addUser(newUser);

        }
    }
}

