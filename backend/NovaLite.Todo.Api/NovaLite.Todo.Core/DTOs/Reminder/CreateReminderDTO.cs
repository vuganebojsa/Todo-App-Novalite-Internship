namespace NovaLite.Todo.Core.DTOs.Reminder
{
    public class CreateReminderDTO
    {
        public DateTimeOffset Timestamp { get; set; }
        public Guid TodoListId { get; set; }

        public CreateReminderDTO(DateTimeOffset timestamp, Guid todoListId)
        {
            Timestamp = timestamp;
            TodoListId = todoListId;
        }
    }
}
