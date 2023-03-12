using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Application.Subtasks.Requests;

namespace ToDoApp.Application.ToDos.Requests
{
    public class ToDoUpdateModel
    {
        public string Title { get; set; }
        public DateTime? TargetCompletionDate { get; set; }
        public List<SubtaskUpdateModelForToDoUpdate> Subtasks { get; set; }
    }
}
