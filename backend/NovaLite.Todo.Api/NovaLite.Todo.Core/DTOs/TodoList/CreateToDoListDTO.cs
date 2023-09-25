using NovaLite.Todo.Core.ErrorHandlers;

namespace NovaLite.Todo.Core.DTOs.TodoList
{
    public class CreateToDoListDTO
    {
        [CustomRequired("Title")]
        [CustomMaxLength("Title", 255)]
        public string Title { get; set; }

        [CustomRequired("Title")]
        public string Description { get; set; }

        public CreateToDoListDTO(string title, string description)
        {
            Title = title;
            Description = description;
        }
    }
}
