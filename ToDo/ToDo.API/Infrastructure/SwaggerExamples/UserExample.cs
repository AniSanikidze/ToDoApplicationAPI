using Swashbuckle.AspNetCore.Filters;
using ToDoApp.Application.ToDos.Responses;
using ToDoApp.Application.Users.Responses;

namespace ToDoApp.API.Infrastructure.SwaggerExamples
{
    public class UserExample : IExamplesProvider<UserResponseModel>
    {
        public UserResponseModel GetExamples()
        {
            return new UserResponseModel
            {
                Id = 1,
                Username = "ExampleUser",
            };
        }
    }
}
