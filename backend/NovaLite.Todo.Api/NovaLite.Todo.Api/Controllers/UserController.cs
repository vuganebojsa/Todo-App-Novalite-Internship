using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NovaLite.Todo.Api.Exceptions;
using NovaLite.Todo.Core.DTOs;
using NovaLite.Todo.Core.Interfaces;

namespace NovaLite.Todo.Api.Controllers
{

    [Route("api/v1/users")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var user = await _userService.Login(loginDto);

            return Ok(user);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllUsers()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var email = await GetUserEmail();
                List<UserDTO> users = await _userService.GetAllUsers(email);
                return Ok(users);
            }
            catch (NotFoundException exc)
            {
                return NotFound(exc.Message);
            }
        }


        [HttpGet("admin")]
        [Authorize]
        public async Task<IActionResult> GetAllUsersWithAdmin()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var email = await GetUserEmail();
                List<UserDTO> users = await _userService.GetAllUsersWithAdmin(email);
                return Ok(users);
            }
            catch (NotFoundException exc)
            {
                return NotFound(exc.Message);
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> ChangeUserRole([FromBody] UserRoleDTO userRole)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var email = await GetUserEmail();
                UserDTO user = await _userService.ChangeUserRole(userRole, email);
                return Ok(user);
            }
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
            }
        }
    }
}
