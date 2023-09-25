using NovaLite.Todo.Core.DTOs.Attachment;
using NovaLite.Todo.Core.Models;

namespace NovaLite.Todo.Core.DTOs.TodoList
{
    public class ToDoListDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public List<TodoItemDTO> ToDoItems { get; set; }
        public List<TodoAttachmentDTO> ToDoAttachments { get; set; }

        public ToDoListDTO(Guid id, string title, string description)
        {
            Id = id;
            Title = title;
            Description = description;
        }
        public ToDoListDTO(Guid id, string title, string description, List<TodoItemDTO> todoItems)
        {
            Id = id;
            Title = title;
            Description = description;
            ToDoItems = todoItems;
        }
        public ToDoListDTO(ToDoList toDoList)
        {
            Id = toDoList.Id;
            Title = toDoList.Title;
            Description = toDoList.Description;
        }
        public ToDoListDTO(ToDoList toDoList, ICollection<TodoItem> todoItems)
        {
            Id = toDoList.Id;
            Title = toDoList.Title;
            Description = toDoList.Description;
            ToDoItems = new List<TodoItemDTO>();
            foreach (var item in todoItems)
            {
                ToDoItems.Add(new TodoItemDTO(item.Id, item.Content, item.Status));
            }
        }
        public ToDoListDTO(ToDoList toDoList, ICollection<TodoItem> todoItems, ICollection<TodoAttachment> todoAttachments)
        {
            Id = toDoList.Id;
            Title = toDoList.Title;
            Description = toDoList.Description;
            ToDoItems = new List<TodoItemDTO>();
            foreach (var item in todoItems)
            {
                ToDoItems.Add(new TodoItemDTO(item.Id, item.Content, item.Status));
            }
            ToDoAttachments = new List<TodoAttachmentDTO>();
            foreach (var item in todoAttachments)
            {
                ToDoAttachments.Add(new TodoAttachmentDTO(item.Id, item.FileName, item.TodoListId.Value, item.Type));
            }
        }
    }
}
