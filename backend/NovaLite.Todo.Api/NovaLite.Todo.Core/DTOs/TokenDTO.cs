using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaLite.Todo.Core.DTOs
{
    public class TokenDTO
    {
        public string Token { get; set; }
        public TokenDTO(string token)
        {
            Token = token;
        }
    }
}
