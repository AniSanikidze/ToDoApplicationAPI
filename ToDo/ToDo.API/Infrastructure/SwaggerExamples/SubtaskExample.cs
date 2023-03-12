using Swashbuckle.AspNetCore.Filters;
using ToDo.Domain.ToDos;
using ToDoApp.Application.Subtasks.Responses;
using ToDoApp.Application.ToDos.Responses;

namespace ToDoApp.API.Infrastructure.SwaggerExamples
{
    public class SubtaskExample : IExamplesProvider<SubtaskResponseModel>
    {
        public SubtaskResponseModel GetExamples()
        {
            return
                new SubtaskResponseModel
                {
                    Id = 1,
                    Title = "Subtask 1 of ExampleToDo",
                    ToDoItemId = 1
                };
        }
    }
}
