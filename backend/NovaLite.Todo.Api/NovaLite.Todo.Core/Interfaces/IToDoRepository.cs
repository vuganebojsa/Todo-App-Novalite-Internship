using NovaLite.Todo.Core.DTOs;
using NovaLite.Todo.Core.DTOs.Reminder;
using NovaLite.Todo.Core.DTOs.TodoList;
using NovaLite.Todo.Core.Models;

namespace NovaLite.Todo.Core.Interfaces
{
    public interface IToDoRepository
    {
        Task<ToDoList> GetToDoListBy(Guid id, Guid userId);
        Task<List<ToDoList>> GetAllLists(Guid id);
        Task<ToDoList> Create(ToDoList todoList);
        Task<ToDoList> Update(ToDoList toDoListToUpdate, UpdateToDoListDTO newToDoListValues);
        Task Remove(ToDoList todoList);
        Task<TodoItem> GetToDoListItemBy(Guid id);
        Task<ICollection<TodoItem>> GetToDoListItemsByToDoListId(Guid id);
        Task UpdateToDoItem(TodoItem tditem, UpdateToDoListItemDTO toDoItemDTO);
        Task CreateToDoListItem(TodoItem toDoListItem);
        Task AddToDoListItemToList(TodoItem toDoListItem, ToDoList tdl);
        Task CreateReminder(TodoReminder newReminder);
        Task AddReminderToList(TodoReminder newReminder, ToDoList tdl);
        Task<List<TodoReminder>> GetAllActiveReminders();
        Task ChangeReminderStatus(Guid id);
        Task<List<TodoAttachment>> GetAllAttachmentsByTodoListId(Guid id);
        Task CreateAttachment(TodoAttachment newAttachment);
        Task AddToDoListAttachment(TodoAttachment newAttachment, ToDoList tdl);
        Task<TodoAttachment> GetAttachmentBy(Guid id, Guid userId);

        Task<List<ReminderWithUserDTO>> GetRemindersWithUsers();
    }
}
