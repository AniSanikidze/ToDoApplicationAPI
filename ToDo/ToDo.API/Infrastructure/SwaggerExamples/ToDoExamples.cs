using Swashbuckle.AspNetCore.Filters;
using ToDo.Domain.ToDos;
using ToDoApp.Application.Subtasks.Responses;
using ToDoApp.Application.ToDos.Responses;

namespace ToDoApp.API.Infrastructure.SwaggerExamples
{
    public class ToDoExamples : IMultipleExamplesProvider<ToDoResponseModel>
    {
        public IEnumerable<SwaggerExample<ToDoResponseModel>> GetExamples()
        {
            yield return SwaggerExample.Create("example 1", new ToDoResponseModel
            {
                Id = 1,
                Title = "ExampleToDo",
                TargetCompletionDate = DateTime.Now.AddDays(2),
                OwnerId = 1,
                ToDoStatus = ToDoStatuses.Active,
                Subtasks = new List<SubtaskResponseModel>()
                        {
                            new SubtaskResponseModel
                            {
                                Id = 1,
                                Title = "Subtask 1 of ExampleToDo",
                                ToDoItemId = 1
                            },
                            new SubtaskResponseModel
                            {
                                Id = 2,
                                Title = "Subtask 2 of ExampleToDo",
                                ToDoItemId = 1
                            }
                        }
            });
            yield return SwaggerExample.Create("example 2", new ToDoResponseModel
            {
                Id = 2,
                Title = "ExampleToDo2",
                TargetCompletionDate = DateTime.Now.AddDays(2),
                OwnerId = 1,
                ToDoStatus = ToDoStatuses.Active,
                Subtasks = new List<SubtaskResponseModel>()
            });
        }
    }
}
