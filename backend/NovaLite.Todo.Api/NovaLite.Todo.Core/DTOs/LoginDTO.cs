using NovaLite.Todo.Core.ErrorHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NovaLite.Todo.Core.DTOs
{
    public class LoginDTO
    {
        [CustomRequired("Email")]
        [EmailAddress(ErrorMessage = "The Email address is not in the correct format.")]
        public string Email { get; set; }

        public LoginDTO(string email)
        {
            Email = email;
        }
    }
}
