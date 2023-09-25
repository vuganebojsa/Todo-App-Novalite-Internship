using NovaLite.Todo.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaLite.Todo.Core.Interfaces
{
    public interface IUserService
    {
        Task<UserReturnDTO> Login(LoginDTO loginDTO);
        Task<List<UserDTO>> GetAllUsers(string email);
        Task<UserDTO> ChangeUserRole(UserRoleDTO userRole, string email);
        Task<List<UserDTO>> GetAllUsersWithAdmin(string email);
    }
}
