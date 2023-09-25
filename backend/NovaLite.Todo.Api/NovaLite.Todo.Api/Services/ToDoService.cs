using NovaLite.Todo.Api.Exceptions;
using NovaLite.Todo.Api.Messages;
using NovaLite.Todo.Core.DTOs;
using NovaLite.Todo.Core.DTOs.Attachment;
using NovaLite.Todo.Core.DTOs.Reminder;
using NovaLite.Todo.Core.DTOs.TodoList;
using NovaLite.Todo.Core.Interfaces;
using NovaLite.Todo.Core.Models;

namespace NovaLite.Todo.Api.Services
{
    public class ToDoService : ITodoService
    {
        private readonly IToDoRepository _toDoRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBlobService _blobService;
        public ToDoService(IToDoRepository todoRepository, IBlobService blobService, IUserRepository userRepository)
        {
            _toDoRepository = todoRepository;
            _blobService = blobService;
            _userRepository = userRepository;
        }

        public async Task<ToDoListDTO> Create(CreateToDoListDTO todoList, string email)
        {
            var user = await _userRepository.FindUserByEmail(email) ?? throw new NotFoundException(ErrorMessages.NotFound("user", "email"));
            var createdTdl = new ToDoList(todoList.Title, todoList.Description)
            {
                TodoUserId = user.Id
            };
            createdTdl = await _toDoRepository.Create(createdTdl);

            return new ToDoListDTO(createdTdl);
        }

        public async Task<CreateAttachmentResponseDTO> CreateAttachment(CreateAttachmentDTO attachment, string email)
        {
            var user = await _userRepository.FindUserByEmail(email) ?? throw new NotFoundException(ErrorMessages.NotFound("user", "email"));
            var tdl = await _toDoRepository.GetToDoListBy(attachment.ToDoListId, user.Id) ?? throw new NotFoundException(ErrorMessages.NotFound("Todo List", "Id"));
            List<TodoAttachment> tdlAttachments = await _toDoRepository.GetAllAttachmentsByTodoListId(tdl.Id);
            tdlAttachments ??= new List<TodoAttachment>();

            var newAttachment = new TodoAttachment(attachment.FileName, attachment.Type)
            {
                TodoListId = attachment.ToDoListId
            };
            await _toDoRepository.CreateAttachment(newAttachment);
            tdl.Attachments = tdlAttachments;
            await _toDoRepository.AddToDoListAttachment(newAttachment, tdl);

            var sasToken = _blobService.GenerateSaSToken(SasPermission.Create, newAttachment.Id.ToString());

            return new CreateAttachmentResponseDTO(sasToken.ToString(),
                new AttachmentDTO(newAttachment.Id, newAttachment.FileName, tdl.Id, newAttachment.Type));

        }


        public async Task<CreatedToDoListItemDTO> CreateToDoListItem(CreateToDoListItemDTO toDoListItemDTO, string email)
        {
            var user = await _userRepository.FindUserByEmail(email) ?? throw new NotFoundException(ErrorMessages.NotFound("user", "email"));
            var tdl = await _toDoRepository.GetToDoListBy(toDoListItemDTO.ToDoListId, user.Id) ?? throw new NotFoundException(ErrorMessages.NotFound("Todo List", "Id"));
            var tdlItems = await _toDoRepository.GetToDoListItemsByToDoListId(tdl.Id);
            tdlItems ??= new List<TodoItem>();

            // add todo list item to database
            var toDoListItem = new TodoItem(toDoListItemDTO.Content, toDoListItemDTO.status)
            {
                TodoListId = tdl.Id
            };
            await _toDoRepository.CreateToDoListItem(toDoListItem);
            tdl.TodoItems = tdlItems;
            await _toDoRepository.AddToDoListItemToList(toDoListItem, tdl);

            return new CreatedToDoListItemDTO(toDoListItem.Id, toDoListItem.Content, tdl.Id, toDoListItem.Status);
        }

        public async Task<CreatedReminderDTO> CreateToDoListReminder(CreateReminderDTO reminder, string email)
        {
            var user = await _userRepository.FindUserByEmail(email) ?? throw new NotFoundException(ErrorMessages.NotFound("user", "email"));
            var tdl = await _toDoRepository.GetToDoListBy(reminder.TodoListId, user.Id) ?? throw new NotFoundException(ErrorMessages.NotFound("Todo List", "Id"));
            if (tdl.Reminders != null && tdl.Reminders.Any()) throw new BadHttpRequestException(ErrorMessages.CurrentlyActiveReminder());

            if (reminder.Timestamp < DateTime.Now.AddMinutes(1)) throw new BadHttpRequestException(ErrorMessages.InvalidReminderDate());

            TodoReminder newReminder = new(reminder.Timestamp, false)
            {
                TodoListId = tdl.Id
            };

            await _toDoRepository.CreateReminder(newReminder);
            await _toDoRepository.AddReminderToList(newReminder, tdl);
            return new CreatedReminderDTO(newReminder.Id, tdl.Id, newReminder.Timestamp, newReminder.Sent);
        }

        public async Task<bool> Delete(Guid id, string email)
        {
            var user = await _userRepository.FindUserByEmail(email) ?? throw new NotFoundException("The user with the given email doesn't exist");
            var todolist = await _toDoRepository.GetToDoListBy(id, user.Id);
            if (todolist == null || todolist.IsDeleted) throw new NotFoundException("The todo list with the given Id doesn't exist.");
            await _toDoRepository.Remove(todolist);
            return todolist.IsDeleted;
        }

        public async Task<List<ToDoListDTO>> GetAllLists(string email)
        {

            var user = await _userRepository.FindUserByEmail(email) ?? throw new NotFoundException("The user with the given email doesn't exist");
            var lists = await _toDoRepository.GetAllLists(user.Id);
            List<ToDoListDTO> todoLists = new();
            foreach (var list in lists)
            {
                todoLists.Add(new ToDoListDTO(list));
            }
            return todoLists;
        }

        public async Task<string> GetAttachmentToken(Guid id, string email)
        {
            var user = await _userRepository.FindUserByEmail(email) ?? throw new NotFoundException("The user with the given email doesn't exist");
            _ = await _toDoRepository.GetAttachmentBy(id, user.Id) ?? throw new NotFoundException(ErrorMessages.NotFound("Attachment", "Id"));
            var token = _blobService.GenerateSaSToken(SasPermission.Get, id.ToString());
            return token;
        }

        public async Task<ToDoListDTO> GetToDoListBy(Guid id, string email)
        {
            var user = await _userRepository.FindUserByEmail(email) ?? throw new NotFoundException("The user with the given email doesn't exist.");
            var tdl = await _toDoRepository.GetToDoListBy(id, user.Id);
            return tdl == null
                ? throw new NotFoundException(ErrorMessages.NotFound("Todo Item", "Id"))
                : new ToDoListDTO(tdl, tdl.TodoItems, tdl.Attachments);
        }
        public async Task<UpdateToDoListDTO> Update(UpdateToDoListDTO todoList, string email)
        {
            var user = await _userRepository.FindUserByEmail(email) ?? throw new NotFoundException("The user with the given email doesn't exist.");
            var tdl = await _toDoRepository.GetToDoListBy(todoList.Id, user.Id);
            if (tdl == null || tdl.IsDeleted) throw new NotFoundException("The to do list with the given id doesn't exist.");

            tdl = await _toDoRepository.Update(tdl, todoList);

            return new UpdateToDoListDTO(tdl.Id, tdl.Title, tdl.Description);
        }

        public async Task<UpdateToDoListItemDTO> UpdateToDoListItem(UpdateToDoListItemDTO toDoItemDTO, string email)
        {
            var user = await _userRepository.FindUserByEmail(email) ?? throw new NotFoundException("The user with the given email doesn't exist.");
            TodoItem tditem = await _toDoRepository.GetToDoListItemBy(toDoItemDTO.Id) ?? throw new NotFoundException("The List item with the given id doesn't exist.");
            ToDoList tdList = await _toDoRepository.GetToDoListBy(toDoItemDTO.ToDoListId, user.Id) ?? throw new NotFoundException("The ToDo list with the given id doesn't exist.");
            await _toDoRepository.UpdateToDoItem(tditem, toDoItemDTO);
            return new UpdateToDoListItemDTO(tditem.Id, tditem.Content, tditem.Status, tdList.Id);
        }
    }
}
