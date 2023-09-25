using NovaLite.Todo.Core.ErrorHandlers;
using NovaLite.Todo.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaLite.Todo.Core.DTOs
{
    public class UserRoleDTO
    {
        [CustomRequired("Email")]
        [EmailAddress(ErrorMessage = "Email is not in the correct format.")]
        public string Email { get; set; }
        [CustomRequired("Role")]
        public Role role { get; set; }

        public UserRoleDTO(string email, Role role)
        {
            Email = email;
            this.role = role;   
        }
    }
}
