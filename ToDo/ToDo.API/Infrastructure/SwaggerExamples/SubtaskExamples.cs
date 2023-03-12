using Swashbuckle.AspNetCore.Filters;
using ToDo.Domain.ToDos;
using ToDoApp.Application.Subtasks.Responses;
using ToDoApp.Application.ToDos.Responses;

namespace ToDoApp.API.Infrastructure.SwaggerExamples
{
    public class SubtaskExamples : IMultipleExamplesProvider<SubtaskResponseModel>
    {
        public IEnumerable<SwaggerExample<SubtaskResponseModel>> GetExamples()
        {
            yield return SwaggerExample.Create("example 1", new SubtaskResponseModel
            {

                Id = 1,
                Title = "Subtask 1 of ExampleToDo",
                ToDoItemId = 1
            });

            yield return SwaggerExample.Create("example 2", new SubtaskResponseModel
            {

                Id = 2,
                Title = "Subtask 2 of ExampleToDo",
                ToDoItemId = 1
            });
        }
    }
}
