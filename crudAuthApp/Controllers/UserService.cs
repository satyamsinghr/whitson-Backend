namespace crudAuthApp.Controllers
{
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;
    using crudAuthApp.Model;

    public class UserService
    {
        private readonly Context _context;

        public UserService(Context context)
        {
            _context = context;
        }

        public async Task<UserDetail> RegisterUser(string username, string email, string password)
        {
            var existingUser = await _context.UserDetails.FirstOrDefaultAsync(u => u.Email == email);
            if (existingUser != null)
            {
                throw new Exception("Email is already in use.");
            }

            var user = new UserDetail
            {
                Username = username,
                Email = email,
                Password = password
            };

            _context.UserDetails.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<UserDetail> AuthenticateUser(string email, string password)
        {
            var user = await _context.UserDetails.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

            if (user == null)
            {
                return null; // Invalid credentials 
            }

            return user;
        }
        public async Task<UserDetail> GetUserDetails(Guid id)
        {
            var user = await _context.UserDetails.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return null; // Invalid credentials
            }

            return user;
        }
    }

}
