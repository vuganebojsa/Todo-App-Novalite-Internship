using NovaLite.Todo.Core.ErrorHandlers;

namespace NovaLite.Todo.Core.DTOs.TodoList
{
    public class UpdateToDoListDTO
    {
        [CustomRequired("Id")]
        public Guid Id { get; set; }
        [CustomRequired("Title")]
        [CustomMaxLength("Title", 255)]
        public string Title { get; set; }
        [CustomRequired("Description")]

        public string Description { get; set; }

        public UpdateToDoListDTO(Guid id, string title, string description)
        {
            Id = id;
            Title = title;
            Description = description;
        }

    }
}
