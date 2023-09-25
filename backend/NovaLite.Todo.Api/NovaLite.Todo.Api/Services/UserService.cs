using NovaLite.Todo.Api.Exceptions;
using NovaLite.Todo.Api.Messages;
using NovaLite.Todo.Core.DTOs;
using NovaLite.Todo.Core.Interfaces;
using NovaLite.Todo.Core.Models;

namespace NovaLite.Todo.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDTO> ChangeUserRole(UserRoleDTO userRole, string email)
        {
            _ = await _userRepository.GetAdmin(email) ?? throw new NotFoundException(ErrorMessages.NotFound("user", "email"));
            var user = await _userRepository.FindUserByEmail(userRole.Email) ?? throw new NotFoundException(ErrorMessages.NotFound("user", "email"));
            await _userRepository.ChangeUserRole(user, userRole.role);
            return new UserDTO(user.Email, user.Role);
        }


        public async Task<List<UserDTO>> GetAllUsers(string email)
        {
            _ = await _userRepository.GetAdmin(email) ?? throw new NotFoundException(ErrorMessages.NotFound("user", "email"));
            var users = await _userRepository.GetAllUsers();
            return users;
        }

        public async Task<List<UserDTO>> GetAllUsersWithAdmin(string email)
        {
            _ = await _userRepository.GetAdmin(email) ?? throw new NotFoundException(ErrorMessages.NotFound("user", "email"));
            var users = await _userRepository.GetAllUsersWithAdmin(email);
            return users;
        }

        public async Task<UserReturnDTO> Login(LoginDTO loginDTO)
        {
            var user = await _userRepository.FindUserByEmail(loginDTO.Email);

            user ??= await RegisterUser(loginDTO.Email);

            return new UserReturnDTO(user.Role, user.Id);
        }

        private async Task<TodoUser> RegisterUser(string email)
        {
            var user = await _userRepository.Register(email, Role.User);
            return user;
        }
    }
}
