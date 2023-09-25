using NovaLite.Todo.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaLite.Todo.Core.DTOs
{
    public class UserReturnDTO
    {
        public Role role { get; set; }
        public Guid Id { get; set; }

        public UserReturnDTO(Role role, Guid id)
        {
            this.role = role;
            Id = id;
        }
    }
}
