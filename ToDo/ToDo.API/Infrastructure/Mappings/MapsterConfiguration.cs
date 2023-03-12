using Mapster;
using ToDo.Domain.Subtasks;
using ToDo.Domain.ToDos;
using ToDo.Domain.Users;
using ToDoApp.Application.Subtasks.Requests;
using ToDoApp.Application.Subtasks.Responses;
using ToDoApp.Application.ToDos.Responses;
using ToDoApp.Application.Users.Responses;

namespace ToDoApp.API.Infrastructure.Mappings
{
    public static class MapsterConfiguration
    {
        public static void RegisterMaps(this IServiceCollection services)
        {
            TypeAdapterConfig<User, UserResponseModel>
                .NewConfig();
            TypeAdapterConfig<ToDoEntity, ToDoResponseModel>
                .NewConfig();
            TypeAdapterConfig<Subtask, SubtaskResponseModel>
                .NewConfig();
            TypeAdapterConfig<SubtaskRequestModel, Subtask>
                .NewConfig();
                //.Map(dest => dest.ToDoItemId, src => src.ToDoItemId);
            TypeAdapterConfig<Subtask, SubtaskResponseModel>
                .NewConfig();
                //.Map(dest => dest.TodoItemId, src => src.ToDoItemId);

        }
    }
}
