using NovaLite.Todo.Core.Models;

namespace NovaLite.Todo.Core.DTOs
{
    public class CreatedToDoListItemDTO
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public Guid ToDoListId { get; set; }
        public Status status { get; set; }

        public CreatedToDoListItemDTO(Guid id, string content, Guid toDoListId, Status status)
        {
            Id = id;
            Content = content;
            ToDoListId = toDoListId;
            this.status = status;
        }
    }
}
