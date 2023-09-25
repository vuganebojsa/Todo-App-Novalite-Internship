namespace NovaLite.Todo.Core.Models
{
    public enum Status
    {
        Open, Active, Closed
    }
    public class TodoItem
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public Status Status { get; set; }
        public Guid? TodoListId { get; set; }
        public ToDoList TodoList { get; set; }

        public bool IsDeleted { get; set; }
        public TodoItem(string content, Status status)
        {
            Content = content;
            Status = status;
            IsDeleted = false;
        }
    }
}
