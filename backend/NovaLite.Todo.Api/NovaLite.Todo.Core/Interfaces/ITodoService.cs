using NovaLite.Todo.Core.DTOs;
using NovaLite.Todo.Core.DTOs.Attachment;
using NovaLite.Todo.Core.DTOs.Reminder;
using NovaLite.Todo.Core.DTOs.TodoList;

namespace NovaLite.Todo.Core.Interfaces
{
    public interface ITodoService
    {
        Task<ToDoListDTO> GetToDoListBy(Guid id, string email);
        Task<List<ToDoListDTO>> GetAllLists(string email);
        Task<ToDoListDTO> Create(CreateToDoListDTO todoList, string email);
        Task<UpdateToDoListDTO> Update(UpdateToDoListDTO todoList, string email);
        Task<bool> Delete(Guid id, string email);
        Task<UpdateToDoListItemDTO> UpdateToDoListItem(UpdateToDoListItemDTO toDoListDTO, string email);
        Task<CreatedToDoListItemDTO> CreateToDoListItem(CreateToDoListItemDTO toDoListItemDTO, string email);
        Task<CreatedReminderDTO> CreateToDoListReminder(CreateReminderDTO reminder, string email);
        Task<CreateAttachmentResponseDTO> CreateAttachment(CreateAttachmentDTO attachment, string email);
        Task<string> GetAttachmentToken(Guid id, string email);
    }
}
