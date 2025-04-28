
using DOLS.UserMicroService.Models;
using DOLS.UserService.DAL;

namespace DOLS.UserService.Service
{
    public class UsersService
    {

        private readonly UserDAL _userDAL;

        public UsersService(UserDAL userDAL)
        {
            _userDAL = userDAL;
        }


        public User Register(string username, string email, string password)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            var user = new User
            {
                Username = username,
                Email = email,
                Password = hashedPassword
            };

            _userDAL.RegisterUser(user);

            return user;
        }

        public User Login(string username, string password)
        {
            var user = _userDAL.GetUserByUsername(username);

            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid username or password.");
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.Password);

            if (!isPasswordValid)
            {
                throw new UnauthorizedAccessException("Invalid username or password.");
            }

            return user;
        }

        public async Task<Task> InitializeDatabase()
        {

            return _userDAL.CreateTableIfNotExistsAsync();
           
        }
    }
}
