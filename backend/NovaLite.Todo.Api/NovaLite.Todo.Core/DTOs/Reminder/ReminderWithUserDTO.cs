using NovaLite.Todo.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaLite.Todo.Core.DTOs.Reminder
{
    public class ReminderWithUserDTO
    {
        public TodoReminder reminder;
        public string Email { get; set; }

        public ReminderWithUserDTO(TodoReminder reminder, string email)
        {
            this.reminder = reminder;
            Email = email;
        }
    }
}
