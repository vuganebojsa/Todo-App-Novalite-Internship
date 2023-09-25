namespace NovaLite.Todo.Core.Models
{
    public class TodoAttachment
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public Guid? TodoListId { get; set; }
        public ToDoList TodoList { get; set; }
        public bool IsDeleted { get; set; }
        public string Type { get; set; }

        public TodoAttachment(string fileName, string type)
        {
            FileName = fileName;
            IsDeleted = false;
            Type = type;
        }
    }
}
