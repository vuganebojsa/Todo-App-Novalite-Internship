using NovaLite.Todo.Core.Models;

namespace NovaLite.Todo.Core.DTOs
{
    public class TodoItemDTO
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public Status status { get; set; }

        public TodoItemDTO(Guid id, string content, Status status)
        {
            Id = id;
            Content = content;
            this.status = status;
        }
    }
}
