using ToDo.Application;
using ToDo.Domain.Subtasks;
using ToDo.Domain.Users;

namespace ToDo.Domain.ToDos
{
    public class ToDoEntity : BaseEntity
    {
        public string Title { get; set; }
        public ToDoStatuses ToDoStatus { get; set; }
        public DateTime? TargetCompletionDate { get; set; }
        public int OwnerId { get; set; }
        public List<Subtask> Subtasks { get; set; }
        public User User { get; set; }
    }

    public enum ToDoStatuses
    {
        Active = 1,
        Done = 2,
        Deleted = 3
    }
}
