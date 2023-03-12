using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Application.Subtasks.Responses
{
    public class SubtaskResponseModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ToDoItemId { get; set; }
    }
}
