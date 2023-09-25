using FakeItEasy;
using FluentAssertions;
using NovaLite.Todo.Api.Exceptions;
using NovaLite.Todo.Api.Services;
using NovaLite.Todo.Core.Interfaces;
using NovaLite.Todo.Core.Models;

namespace Novalite.Todo.Testing.Services
{
    public class TodoServiceTests
    {
        private readonly ITodoService _service;
        private readonly IUserRepository _userRepository;
        private readonly IToDoRepository _todoRepository;
        private readonly IBlobService _blobService;

        private const string VALID_USER_EMAIL = "nebojsavuga@gmail.com";
        private const string INVALID_USER_EMAIL = "ana@gmail.com";
        private readonly Guid VALID_USER_ID = Guid.NewGuid();
        private readonly Guid INVALID_USER_ID = Guid.NewGuid();
        private readonly TodoUser VALID_USER = new(VALID_USER_EMAIL, Role.User, new());
        private readonly ToDoList VALID_TODO_LIST = new("Listica", "Opis listice");
        private readonly Guid VALID_ATTACHMENT_ID = Guid.NewGuid();
        private readonly Guid INVALID_ATTACHMENT_ID = Guid.NewGuid();
        private readonly Guid VALID_TODO_ITEM_ID = Guid.NewGuid();
        private readonly Guid VALID_TODOLIST_ID = Guid.NewGuid();
        private readonly TodoItem VALID_TODO_ITEM = new("content", Status.Open);
        private readonly TodoAttachment VALID_ATTACHMENT = new("content", "content");

        public TodoServiceTests()
        {
            _userRepository = A.Fake<IUserRepository>();
            _todoRepository = A.Fake<IToDoRepository>();
            _blobService = A.Fake<IBlobService>();
            _service = new ToDoService(_todoRepository, _blobService, _userRepository);

            // setup fake returns
            SetupUserRepositoryFakes();
            SetupBlobServiceFakes();
            SetupTodoRepositoryFakes();
            VALID_USER.Id = VALID_USER_ID;
        }

        [Fact]
        public async Task GetAttachmentToken_ShouldReturnTokenWithValidInfo()
        {
            var token = await _service.GetAttachmentToken(VALID_ATTACHMENT_ID, VALID_USER_EMAIL);
            token.Should().BeEquivalentTo("fakedtoken");
        }

        [Fact]
        public async Task GetAttachmentToken_ShouldThrowExceptionOnInvalidUserEmail()
        {

            try
            {
                var token = await _service.GetAttachmentToken(VALID_ATTACHMENT_ID, INVALID_USER_EMAIL);
            }
            catch (NotFoundException exc)
            {
                exc.Message.Should().BeEquivalentTo("The user with the given email doesn't exist");
                A.CallTo(() => _todoRepository.GetAttachmentBy(A<Guid>.Ignored, A<Guid>.Ignored)).MustNotHaveHappened();

            }
        }
        [Fact]
        public async Task GetAttachmentToken_ShouldThrowExceptionOnInvalidAttachmentId()
        {

            try
            {
                var token = await _service.GetAttachmentToken(INVALID_ATTACHMENT_ID, VALID_USER_EMAIL);
            }
            catch (NotFoundException exc)
            {
                exc.Message.Should().BeEquivalentTo("The atachment with the given id doesn't exist.");
                A.CallTo(() => _todoRepository.GetAttachmentBy(A<Guid>.Ignored, A<Guid>.Ignored)).MustHaveHappened();
                A.CallTo(() => _blobService.GenerateSaSToken(A<SasPermission>.Ignored, A<string>.Ignored)).MustNotHaveHappened();
            }
        }

        [Fact]
        public async Task GetAllLists_ShouldReturnListsWithValidEmail()
        {
            var response = await _service.GetAllLists(VALID_USER_EMAIL);

            response.Should().NotBeEmpty();
            response.Should().HaveCount(1);
            A.CallTo(() => _todoRepository.GetAllLists(A<Guid>.Ignored)).MustHaveHappened();

        }
        [Fact]
        public async Task GetAllLists_ShouldThrowExceptionWithInvalidEmail()
        {
            try
            {
                var response = await _service.GetAllLists(INVALID_USER_EMAIL);
            }
            catch (NotFoundException exc)
            {
                exc.Message.Should().BeEquivalentTo("The user with the given email doesn't exist");
                A.CallTo(() => _todoRepository.GetAllLists(A<Guid>.Ignored)).MustNotHaveHappened();
            }
        }
        [Fact]
        public async Task GetToDoListBy_ShouldReturnListWithValidParameters()
        {
            var response = await _service.GetToDoListBy(VALID_TODOLIST_ID, VALID_USER_EMAIL);

            response.Should().NotBeNull();
            A.CallTo(() => _userRepository.FindUserByEmail(VALID_USER_EMAIL)).MustHaveHappened();
            A.CallTo(() => _todoRepository.GetToDoListBy(A<Guid>.Ignored, A<Guid>.Ignored)).MustHaveHappened();

        }
        [Fact]
        public async Task GetToDoListBy_ShouldThrowExceptionOnInvalidParams()
        {
            try
            {
                var response = await _service.GetToDoListBy(INVALID_ATTACHMENT_ID, VALID_USER_EMAIL);
            }
            catch
            {
                A.CallTo(() => _userRepository.FindUserByEmail(VALID_USER_EMAIL)).MustHaveHappened();
                A.CallTo(() => _todoRepository.GetToDoListBy(A<Guid>.Ignored, A<Guid>.Ignored)).MustHaveHappened();
            }
        }

        private void SetupUserRepositoryFakes()
        {
            A.CallTo(() => _userRepository.FindUserByEmail(VALID_USER_EMAIL)).Returns(VALID_USER);
            A.CallTo(() => _userRepository.FindUserByEmail(INVALID_USER_EMAIL)).Returns(Task.FromResult<TodoUser>(null));
        }
        private void SetupBlobServiceFakes()
        {
            A.CallTo(() => _blobService.GenerateSaSToken(A<SasPermission>.Ignored, A<string>.Ignored)).Returns("fakedtoken");
        }

        private void SetupTodoRepositoryFakes()
        {
            SetupTodoListFakes();
            SetupTodoItemFakes();
            SetupTodoReminderFakes();
            SetupTodoAttachmentFakes();
        }

        private void SetupTodoAttachmentFakes()
        {
            A.CallTo(() => _todoRepository.GetAllAttachmentsByTodoListId(VALID_TODOLIST_ID)).Returns(
                new List<TodoAttachment>()
                {
                    new("somename", "type")
                });
            A.CallTo(() => _todoRepository.CreateAttachment(A<TodoAttachment>.Ignored)).Invokes(() => { });
            A.CallTo(() => _todoRepository.GetAttachmentBy(VALID_ATTACHMENT_ID, VALID_USER_ID)).Returns(VALID_ATTACHMENT);
            A.CallTo(() => _todoRepository.GetAttachmentBy(INVALID_ATTACHMENT_ID, VALID_USER_ID)).Returns(Task.FromResult<TodoAttachment>(null));
        }

        private void SetupTodoReminderFakes()
        {
            A.CallTo(() => _todoRepository.CreateReminder(A<TodoReminder>.Ignored)).Invokes(() => { });
        }

        private void SetupTodoItemFakes()
        {
            A.CallTo(() => _todoRepository.GetToDoListItemBy(VALID_TODO_ITEM_ID)).Returns(VALID_TODO_ITEM);
            A.CallTo(() => _todoRepository.GetToDoListItemsByToDoListId(VALID_TODOLIST_ID)).Returns(new List<TodoItem>()
            {
                VALID_TODO_ITEM
            });
        }

        private void SetupTodoListFakes()
        {
            A.CallTo(() => _todoRepository.Create(A<ToDoList>.Ignored)).Returns(VALID_TODO_LIST);
            A.CallTo(() => _todoRepository.GetToDoListBy(VALID_ATTACHMENT_ID, VALID_USER_ID)).Returns(VALID_TODO_LIST);
            A.CallTo(() => _todoRepository.GetToDoListBy(INVALID_ATTACHMENT_ID, INVALID_USER_ID)).Returns(Task.FromResult<ToDoList>(null));
            A.CallTo(() => _todoRepository.AddToDoListAttachment(A<TodoAttachment>.Ignored, A<ToDoList>.Ignored)).Invokes(() => { });
            A.CallTo(() => _todoRepository.AddToDoListItemToList(A<TodoItem>.Ignored, A<ToDoList>.Ignored)).Invokes(() => { });
            A.CallTo(() => _todoRepository.AddReminderToList(A<TodoReminder>.Ignored, A<ToDoList>.Ignored)).Invokes(() => { });
            A.CallTo(() => _todoRepository.GetAllLists(VALID_USER_ID)).Returns(new List<ToDoList>()
            {
                VALID_TODO_LIST
            });
        }
    }
}
