using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Application.Subtasks.Requests
{
    public class SubtaskUpdateModelForToDoUpdate
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
