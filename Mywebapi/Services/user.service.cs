
using Microsoft.EntityFrameworkCore;
using Mywebapi.Models;
using Mywebapi.Dtos.User.Request;

namespace Mywebapi.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllUsers(int page, int limit)
        {
            return await _context.Users
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<User?> GetUserById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> CreateUser(CreateUserRequest request)
        {
            bool userIsExisted=await _context.Users.AnyAsync(u=>u.Username==request.Username||u.Email==request.Email);
            if (userIsExisted) {
                return null;
            }
            var user = new User
            {
                Username = request.Username,
                Fullname = request.Fullname,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address,
                Role = request.Role,
                DepartmentId = request.DepartmentId,
                CreateAt = request.CreateAt ?? DateTime.UtcNow
            };

            user.SetPassword(request.Password);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> UpdateUser(int id, UpdateUserRequest request)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return false;

            user.Username = request.Username ?? user.Username;
            user.Fullname = request.Fullname ?? user.Fullname;
            user.Email = request.Email ?? user.Email;
            user.PhoneNumber = request.PhoneNumber ?? user.PhoneNumber;
            user.Address = request.Address ?? user.Address;
            user.Role = request.Role ?? user.Role;
            user.DepartmentId = request.DepartmentId ?? user.DepartmentId;

            if (!string.IsNullOrEmpty(request.Password))
            {
                user.SetPassword(request.Password);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
