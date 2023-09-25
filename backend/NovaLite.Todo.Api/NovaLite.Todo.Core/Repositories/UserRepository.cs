using Microsoft.EntityFrameworkCore;
using NovaLite.Todo.Core.Data;
using NovaLite.Todo.Core.DTOs;
using NovaLite.Todo.Core.Interfaces;
using NovaLite.Todo.Core.Models;

namespace NovaLite.Todo.Core.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task ChangeUserRole(TodoUser user, Role role)
        {
            user.Role = role;
            await _context.SaveChangesAsync();
        }

        public async Task<TodoUser> FindUserByEmail(string email)
        {
            var user = await _context.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
            return user;
        }

        public async Task<TodoUser> GetAdmin(string email)
        {
            var admin = await _context.Users.Where(u => u.Role == Role.Admin && u.Email == email).FirstOrDefaultAsync();
            return admin;
        }

        public async Task<List<UserDTO>> GetAllUsers()
        {
            var users = await _context.Users.Where(u => u.Role == Role.User).ToListAsync();
            var filteredUsers = new List<UserDTO>();
            foreach (var user in users)
            {
                filteredUsers.Add(new UserDTO(user.Email, user.Role));
            }
            return filteredUsers;
        }

        public async Task<List<UserDTO>> GetAllUsersWithAdmin(string email)
        {
            var users = await _context.Users.Where(u => u.Email != email).ToListAsync();
            var filteredUsers = new List<UserDTO>();
            foreach (var user in users)
            {
                filteredUsers.Add(new UserDTO(user.Email, user.Role));
            }
            return filteredUsers;
        }

        public async Task<TodoUser> Register(string email, Role role)
        {
            TodoUser user = new(email, role, new List<ToDoList>());
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
