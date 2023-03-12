using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Application.Subtasks.Requests
{
    public class SubtaskRequestModel
    {
        public string Title { get; set; }
        public int ToDoItemId { get; set; }
    }
}
