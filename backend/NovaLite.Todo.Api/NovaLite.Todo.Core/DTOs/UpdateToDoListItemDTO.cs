using NovaLite.Todo.Core.ErrorHandlers;
using NovaLite.Todo.Core.Models;

namespace NovaLite.Todo.Core.DTOs
{
    public class UpdateToDoListItemDTO
    {
        [CustomRequired("Id")]
        public Guid Id { get; set; }

        [CustomRequired("Content")]
        public string Content { get; set; }

        [CustomRequired("Status")]
        public Status status { get; set; }

        [CustomRequired("ToDoListId")]
        public Guid ToDoListId { get; set; }

        public UpdateToDoListItemDTO(Guid id, string content, Status status, Guid toDoListId)
        {
            Id = id;
            Content = content;
            this.status = status;
            ToDoListId = toDoListId;
        }
    }
}
