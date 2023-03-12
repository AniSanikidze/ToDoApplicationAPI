using ToDo.Domain.ToDos;
using ToDoApp.Application.Subtasks.Responses;

namespace ToDoApp.Application.ToDos.Responses
{
    public class ToDoResponseModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? TargetCompletionDate { get; set; }
        public ToDoStatuses ToDoStatus { get; set; }   
        public int OwnerId { get; set; }
        public List<SubtaskResponseModel> Subtasks { get; set; }
    }
}
