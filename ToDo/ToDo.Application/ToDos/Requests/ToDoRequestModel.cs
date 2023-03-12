using System;
using System.Collections.Generic;
using ToDoApp.Application.Subtasks.Requests;

namespace ToDoApp.Application.ToDos.Requests
{
    public class ToDoRequestModel
    {
        public string Title { get; set; }
        public DateTime? TargetCompletionDate { get; set; }
        public List<SubtaskRequestModelForToDoInsert> Subtasks { get; set; }
    }
}
