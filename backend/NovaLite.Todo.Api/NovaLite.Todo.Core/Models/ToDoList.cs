namespace NovaLite.Todo.Core.Models
{
    public class ToDoList
    {

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<TodoItem> TodoItems { get; set; } = new List<TodoItem>();
        public ICollection<TodoReminder> Reminders { get; set; } = new List<TodoReminder>();
        public ICollection<TodoAttachment> Attachments { get; set; } = new List<TodoAttachment>();

        public Guid? TodoUserId { get; set; }

        public TodoUser TodoUser { get; set; }

        public ToDoList(string title, string description)
        {
            Title = title;
            Description = description;
            IsDeleted = false;
        }
        public ToDoList(string title, string description, ICollection<TodoItem> toDoItems, ICollection<TodoReminder> reminders, ICollection<TodoAttachment> attachments)
        {
            Title = title;
            Description = description;
            TodoItems = toDoItems;
            Reminders = reminders;
            Attachments = attachments;
            IsDeleted = false;
        }

    }
}
