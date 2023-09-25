using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaLite.Todo.Core.DTOs
{
    public class UserClaimsDTO
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public UserClaimsDTO(string userId, string userName)
        {
            UserId = userId;
            UserName = userName;
        }

    }
}
