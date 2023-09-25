using NovaLite.Todo.Core.DTOs;
using NovaLite.Todo.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaLite.Todo.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<TodoUser> FindUserByEmail(string email);
        Task<TodoUser> Register(string email, Role role);
        Task<List<UserDTO>> GetAllUsers();
        Task ChangeUserRole(TodoUser user, Role role);
        Task<TodoUser> GetAdmin(string email);
        Task<List<UserDTO>> GetAllUsersWithAdmin(string email);
    }
}
