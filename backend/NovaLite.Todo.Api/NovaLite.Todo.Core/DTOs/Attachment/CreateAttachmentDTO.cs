using NovaLite.Todo.Core.ErrorHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaLite.Todo.Core.DTOs.Attachment
{
    public class CreateAttachmentDTO
    {
        [CustomRequired("Filename")]
        [CustomMaxLength("Filename", 255)]
        public string FileName { get; set; }
        [CustomRequired("Type")]
        public string Type { get; set; }

        [CustomRequired("TodolistId")]
        public Guid ToDoListId { get; set; }
        public CreateAttachmentDTO(string fileName, string type, Guid toDoListId)
        {
            FileName = fileName;
            ToDoListId = toDoListId;
            Type = type;
        }
    }
}
