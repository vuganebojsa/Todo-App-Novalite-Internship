using NovaLite.Todo.Core.ErrorHandlers;
using NovaLite.Todo.Core.Models;

namespace NovaLite.Todo.Core.DTOs
{
    public class CreateToDoListItemDTO
    {
        [CustomRequired("Content")]
        public string Content { get; set; }

        [CustomRequired("Status")]
        public Status status { get; set; }

        [CustomRequired("ToDoListId")]
        public Guid ToDoListId { get; set; }

        public CreateToDoListItemDTO(string content, Status status, Guid toDoListId)
        {
            Content = content;
            this.status = status;
            ToDoListId = toDoListId;
        }
    }
}
