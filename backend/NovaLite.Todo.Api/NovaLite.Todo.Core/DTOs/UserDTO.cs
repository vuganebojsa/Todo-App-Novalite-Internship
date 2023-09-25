using NovaLite.Todo.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaLite.Todo.Core.DTOs
{
    public class UserDTO
    {
        public string Email { get; set; }
        public Role role { get; set; }

        public UserDTO(string email, Role role)
        {
            Email = email;
            this.role = role;   
        }
    }
}
