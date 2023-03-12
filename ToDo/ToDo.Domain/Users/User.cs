using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application;
using ToDo.Domain.ToDos;

namespace ToDo.Domain.Users
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public List<ToDoEntity> ToDos { get; set; }
    }
}
