using FirstDotNet.Repositories;

namespace FirstDotNet.Services
{

    public interface IUserService
    {
        bool IsValidUser(string username, string password);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool IsValidUser(string username, string password)
        {
            return _userRepository.IsValidUser(username, password);
        }
    }
}

