using Microsoft.EntityFrameworkCore;
using NovaLite.Todo.Core.Data;
using NovaLite.Todo.Core.DTOs;
using NovaLite.Todo.Core.DTOs.Reminder;
using NovaLite.Todo.Core.DTOs.TodoList;
using NovaLite.Todo.Core.Interfaces;
using NovaLite.Todo.Core.Models;

namespace NovaLite.Todo.Core.Repositories
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly DataContext _context;
        //private readonly IHttpContextAccessor _http;
        public ToDoRepository(DataContext context/*, IHttpContextAccessor http*/)
        {
            _context = context;
            //_http = http;
        }

        public async Task AddReminderToList(TodoReminder newReminder, ToDoList tdl)
        {
            tdl.Reminders.Add(newReminder);
            await _context.SaveChangesAsync();
        }

        public async Task AddToDoListAttachment(TodoAttachment newAttachment, ToDoList tdl)
        {
            tdl.Attachments.Add(newAttachment);
            await _context.SaveChangesAsync();
        }

        public async Task AddToDoListItemToList(TodoItem toDoListItem, ToDoList tdl)
        {
            tdl.TodoItems.Add(toDoListItem);
            await _context.SaveChangesAsync();
        }

        public async Task ChangeReminderStatus(Guid id)
        {
            var reminder = await _context.Reminders.Where(rmd => rmd.Id == id).FirstOrDefaultAsync();
            if (reminder != null)
            {
                reminder.Sent = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ToDoList> Create(ToDoList todoList)
        {
            await _context.ToDoLists.AddAsync(todoList);
            await _context.SaveChangesAsync();
            return todoList;
        }

        public async Task CreateAttachment(TodoAttachment newAttachment)
        {
            await _context.Attachments.AddAsync(newAttachment);
            await _context.SaveChangesAsync();
        }

        public async Task CreateReminder(TodoReminder newReminder)
        {
            await _context.Reminders.AddAsync(newReminder);
            await _context.SaveChangesAsync();
        }

        public async Task CreateToDoListItem(TodoItem toDoListItem)
        {
            await _context.ToDoItems.AddAsync(toDoListItem);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TodoReminder>> GetAllActiveReminders()
        {
            var reminders = await _context.Reminders.Where(rmd => rmd.Sent == false).ToListAsync();
            return reminders;
        }

        public async Task<List<TodoAttachment>> GetAllAttachmentsByTodoListId(Guid id)
        {
            var attachments = await _context.Attachments.Where(att => att.IsDeleted == false && att.TodoListId == id && att.TodoList.IsDeleted == false).ToListAsync();
            return attachments;
        }

        public async Task<List<ToDoList>> GetAllLists(Guid id)
        {
            var lists = await _context.ToDoLists.Where(tdl => tdl.IsDeleted == false && tdl.TodoUserId == id).ToListAsync();
            return lists;
        }

        public async Task<TodoAttachment> GetAttachmentBy(Guid id, Guid userId)
        {
            var att = await _context.Attachments.Where(att => att.Id == id && att.IsDeleted == false && att.TodoList.TodoUserId == userId).FirstOrDefaultAsync();
            return att;
        }

        public async Task<List<ReminderWithUserDTO>> GetRemindersWithUsers()
        {

            var reminders = await _context.Reminders.Where(rmd => rmd.Sent == false).ToListAsync();
            var reminderWithUsers = new List<ReminderWithUserDTO>();
            foreach (var reminder in reminders)
            {
                var lst = await _context.ToDoLists.Where(tdl => reminder.TodoListId == tdl.Id).FirstOrDefaultAsync();
                if (lst == null) continue;
                var user = await _context.Users.Where(usr => usr.Id == lst.TodoUserId).FirstOrDefaultAsync();
                if (user == null) continue;
                reminderWithUsers.Add(new ReminderWithUserDTO(reminder, user.Email));

            }
            return reminderWithUsers;
        }

        public async Task<ToDoList> GetToDoListBy(Guid id, Guid userId)
        {
            var tdl = await _context.ToDoLists.Where(td => td.Id == id && td.IsDeleted == false && td.TodoUserId == userId).Include(td => td.TodoItems).Include(td => td.Reminders).Include(td => td.Attachments).FirstOrDefaultAsync();
            return tdl;
        }

        public async Task<TodoItem> GetToDoListItemBy(Guid id)
        {
            var item = await _context.ToDoItems.Where(td => td.Id == id && td.IsDeleted == false).FirstOrDefaultAsync();
            return item;
        }

        public async Task<ICollection<TodoItem>> GetToDoListItemsByToDoListId(Guid id)
        {

            var items = await _context.ToDoItems.Where(td => td.TodoListId == id && td.IsDeleted == false).ToListAsync();
            return items;
        }

        public async Task Remove(ToDoList todoList)
        {
            todoList.IsDeleted = true;
            await _context.SaveChangesAsync();

        }

        public async Task<ToDoList> Update(ToDoList toDoListToUpdate, UpdateToDoListDTO newToDoListValues)
        {
            toDoListToUpdate.Title = newToDoListValues.Title;
            toDoListToUpdate.Description = newToDoListValues.Description;
            await _context.SaveChangesAsync();
            return toDoListToUpdate;
        }

        public async Task UpdateToDoItem(TodoItem tditem, UpdateToDoListItemDTO toDoItemDTO)
        {
            tditem.Status = toDoItemDTO.status;
            tditem.Content = toDoItemDTO.Content;
            await _context.SaveChangesAsync();

        }
    }
}
