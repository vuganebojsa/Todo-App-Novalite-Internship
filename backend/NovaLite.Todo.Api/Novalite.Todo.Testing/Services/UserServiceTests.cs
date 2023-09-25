using FakeItEasy;
using FluentAssertions;
using NovaLite.Todo.Api.Exceptions;
using NovaLite.Todo.Api.Services;
using NovaLite.Todo.Core.DTOs;
using NovaLite.Todo.Core.Interfaces;
using NovaLite.Todo.Core.Models;


namespace Novalite.Todo.Testing.Services
{
    public class UserServiceTests
    {
        private readonly IUserService _service;
        private readonly IUserRepository _repository;

        public UserServiceTests()
        {
            _repository = A.Fake<IUserRepository>();
            _service = new UserService(_repository);
        }

        [Fact]
        public async Task GetAllusersWithAdmin_ShouldReturnUsers()
        {
            string adminEmail = "nebojsa@gmail.com";
            A.CallTo(() => _repository.GetAdmin(adminEmail)).Returns(new TodoUser(adminEmail, Role.Admin, new()));
            A.CallTo(() => _repository.GetAllUsersWithAdmin(adminEmail)).Returns(new List<UserDTO>()
            {
                new UserDTO("neki@gmail.com", Role.User),
                new UserDTO("drugi@gmail.com", Role.Admin),
            });

            var users = await _service.GetAllUsersWithAdmin(adminEmail);

            users.Count.Should().Be(2);
            A.CallTo(() => _repository.GetAdmin(A<string>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _repository.GetAllUsersWithAdmin(A<string>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GetAllusersWithAdmin_ShouldThrowNotFoundException_OnInvalidAdminEmail()
        {
            string adminEmail = "nebojsa@gmail.com";
            A.CallTo(() => _repository.GetAdmin(adminEmail)).Throws(new NotFoundException("The user with the given email doesn't exist."));
            try
            {
                var users = await _service.GetAllUsersWithAdmin(adminEmail);
            }
            catch (NotFoundException)
            {
                A.CallTo(() => _repository.GetAdmin(A<string>.Ignored)).MustHaveHappenedOnceExactly();
                A.CallTo(() => _repository.GetAllUsersWithAdmin(A<string>.Ignored)).MustNotHaveHappened();
            }

        }

        [Fact]
        public async Task GetAllUsers_ShouldReturnUsers()
        {
            string adminEmail = "nebojsa@gmail.com";
            A.CallTo(() => _repository.GetAdmin(adminEmail)).Returns(new TodoUser(adminEmail, Role.Admin, new()));
            A.CallTo(() => _repository.GetAllUsers()).Returns(new List<UserDTO>()
            {
                new UserDTO("neki@gmail.com", Role.User),
                new UserDTO("drugi@gmail.com", Role.User),
            });

            var users = await _service.GetAllUsers(adminEmail);

            users.Count.Should().Be(2);
            A.CallTo(() => _repository.GetAdmin(A<string>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _repository.GetAllUsers()).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GetAllUsers_ShouldThrowNotFoundException_OnInvalidAdminEmail()
        {
            string adminEmail = "nebojsa@gmail.com";
            A.CallTo(() => _repository.GetAdmin(adminEmail)).Throws(new NotFoundException("The user with the given email doesn't exist."));
            try
            {
                var users = await _service.GetAllUsers(adminEmail);
            }
            catch (NotFoundException)
            {
                A.CallTo(() => _repository.GetAdmin(A<string>.Ignored)).MustHaveHappenedOnceExactly();
                A.CallTo(() => _repository.GetAllUsers()).MustNotHaveHappened();
            }
        }

        [Fact]
        public async Task Login_ShouldLoginOnValidInput()
        {
            var logininfo = new LoginDTO("nebojsavuga01@gmail.com");
            A.CallTo(() => _repository.FindUserByEmail(logininfo.Email)).Returns(new TodoUser(logininfo.Email, Role.User, new()));

            var loggedInUser = await _service.Login(logininfo);
            loggedInUser.Should().NotBe(null);
            loggedInUser.role.Should().Be(Role.User);
            A.CallTo(() => _repository.FindUserByEmail(A<string>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _repository.Register(A<string>.Ignored, A<Role>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public async Task Login_ShouldLoginOnValidInputAndRegister()
        {
            var logininfo = new LoginDTO("nebojsavuga01@gmail.com");
            A.CallTo(() => _repository.FindUserByEmail(logininfo.Email)).Returns(Task.FromResult<TodoUser>(null));
            A.CallTo(() => _repository.Register(logininfo.Email, Role.User)).Returns(new TodoUser(logininfo.Email, Role.User, new()));

            var loggedInUser = await _service.Login(logininfo);
            loggedInUser.Should().NotBe(null);
            loggedInUser.role.Should().Be(Role.User);
            A.CallTo(() => _repository.FindUserByEmail(A<string>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _repository.Register(A<string>.Ignored, A<Role>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ChangeUserRole_ShouldChangeOnValidInput()
        {
            var userRoleDTO = new UserRoleDTO("nebojsavuga@gmail.com", Role.Admin);
            string adminEmail = "nebojsavuga01@gmail.com";
            var user = new TodoUser(userRoleDTO.Email, Role.User, new());
            A.CallTo(() => _repository.FindUserByEmail(userRoleDTO.Email)).Returns(user);
            A.CallTo(() => _repository.GetAdmin(adminEmail)).Returns(new TodoUser(adminEmail));
            A.CallTo(() => _repository.ChangeUserRole(user, userRoleDTO.role)).Invokes(() => user.Role = userRoleDTO.role);

            var changedUser = await _service.ChangeUserRole(userRoleDTO, adminEmail);

            changedUser.Should().NotBe(null);
            changedUser.role.Should().Be(userRoleDTO.role);
            A.CallTo(() => _repository.FindUserByEmail(A<string>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _repository.GetAdmin(A<string>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _repository.ChangeUserRole(A<TodoUser>.Ignored, A<Role>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ChangeUserRole_ShouldThrowNotFound_OnInvalidAdminEmail()
        {
            var userRoleDTO = new UserRoleDTO("nebojsavuga@gmail.com", Role.Admin);
            string adminEmail = "nebojsavuga01@gmail.com";
            var user = new TodoUser(userRoleDTO.Email, Role.User, new());
            A.CallTo(() => _repository.GetAdmin(adminEmail)).Returns(Task.FromResult<TodoUser>(null));

            try
            {
                var changedUser = await _service.ChangeUserRole(userRoleDTO, adminEmail);
            }
            catch (NotFoundException)
            {
                A.CallTo(() => _repository.GetAdmin(A<string>.Ignored)).MustHaveHappenedOnceExactly();

                A.CallTo(() => _repository.FindUserByEmail(A<string>.Ignored)).MustNotHaveHappened();
                A.CallTo(() => _repository.ChangeUserRole(A<TodoUser>.Ignored, A<Role>.Ignored)).MustNotHaveHappened();
            }

        }
        [Fact]
        public async Task ChangeUserRole_ShouldThrowNotFound_OnInvalidUserEmail()
        {
            var userRoleDTO = new UserRoleDTO("nebojsavuga@gmail.com", Role.Admin);
            string adminEmail = "nebojsavuga01@gmail.com";
            var user = new TodoUser(userRoleDTO.Email, Role.User, new());
            A.CallTo(() => _repository.FindUserByEmail(userRoleDTO.Email)).Returns(Task.FromResult<TodoUser>(null));
            A.CallTo(() => _repository.GetAdmin(adminEmail)).Returns(new TodoUser(adminEmail));

            try
            {
                var changedUser = await _service.ChangeUserRole(userRoleDTO, adminEmail);
            }
            catch (NotFoundException)
            {
                A.CallTo(() => _repository.GetAdmin(adminEmail)).MustHaveHappenedOnceExactly();

                A.CallTo(() => _repository.FindUserByEmail(A<string>.Ignored)).MustHaveHappenedOnceExactly();
                A.CallTo(() => _repository.ChangeUserRole(A<TodoUser>.Ignored, A<Role>.Ignored)).MustNotHaveHappened();
            }

        }

    }
}
