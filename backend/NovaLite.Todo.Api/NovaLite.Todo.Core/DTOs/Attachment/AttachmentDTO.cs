namespace NovaLite.Todo.Core.DTOs.Attachment
{
    public class AttachmentDTO
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public Guid ToDoListId { get; set; }
        public string Type { get; set; }

        public AttachmentDTO(Guid id, string fileName, Guid toDoListId, string type)
        {
            Id = id;
            FileName = fileName;
            ToDoListId = toDoListId;
            Type = type;
        }
    }
}
