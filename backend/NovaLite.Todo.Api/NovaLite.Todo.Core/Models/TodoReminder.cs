namespace NovaLite.Todo.Core.Models
{
    public class TodoReminder
    {
        public Guid Id { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public bool Sent { get; set; }
        public Guid? TodoListId { get; set; }
        public ToDoList TodoList { get; set; }

        public TodoReminder(DateTimeOffset timestamp, bool sent)
        {
            Timestamp = timestamp;
            Sent = sent;
        }
    }
}
