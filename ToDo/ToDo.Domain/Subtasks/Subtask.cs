using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application;
using ToDo.Domain.ToDos;

namespace ToDo.Domain.Subtasks
{
    public class Subtask : BaseEntity
    {
        public string Title { get; set; }
        public int ToDoItemId { get; set; }
        public ToDoEntity ToDo { get; set; }
    }
}
