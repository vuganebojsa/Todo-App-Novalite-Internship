using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaLite.Todo.Core.DTOs.Attachment
{
    public class TodoAttachmentDTO
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public Guid ToDoListId { get; set; }
        public string Type { get; set; }
        public TodoAttachmentDTO(Guid id, string fileName, Guid toDoListId, string type)
        {
            Id = id;
            FileName = fileName;
            ToDoListId = toDoListId;
            Type = type;
        }
    }
}
