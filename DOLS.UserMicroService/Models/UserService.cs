using DOLS.UserMicroService.Data;
using DOLS.UserMicroService.DTO;
using DOLS.UserMicroService.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DOLS.UserMicroService.Models
{
    public class UserService
    {
        private readonly UserDbContext _context;

        public UserService(UserDbContext context)
        {
            _context = context;
        }

        public async Task<bool> RegisterUserAsync(RegisterRequest request)
        {
            var exists = await _context.Users.AnyAsync(u => u.Username == request.Username);
            if (exists)
                return false;

            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                Password = request.Password
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }
        
        public async Task<User?> LoginUserAsync(LoginRequest request)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == request.Username && u.Password == request.Password);

            return user;
        }
    }
}