namespace NovaLite.Todo.Core.DTOs.Reminder
{
    public class CreatedReminderDTO
    {
        public Guid Id { get; set; }
        public Guid ToDoListId { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public bool Sent { get; set; }

        public CreatedReminderDTO(Guid id, Guid toDoListId, DateTimeOffset timestamp, bool sent)
        {
            Id = id;
            ToDoListId = toDoListId;
            Timestamp = timestamp;
            Sent = sent;
        }
    }
}
