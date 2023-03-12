using ToDo.Infrastructure;
using ToDoApp.Application.Subtasks;
using ToDoApp.Application.Subtasks.Repositories;
using ToDoApp.Application.ToDos;
using ToDoApp.Application.ToDos.Repositories;
using ToDoApp.Application.Users;
using ToDoApp.Application.Users.Repositores;
using ToDoApp.Infrastructure;
using ToDoApp.Infrastructure.Subtasks;
using ToDoApp.Infrastructure.ToDos;
using ToDoApp.Infrastructure.Users;

namespace ToDoApp.API.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IToDoService, ToDoService>();
            services.AddScoped<ISubtaskService, SubtaskService>();
            services.AddScoped<IToDoRepository, ToDoRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISubtaskRepository, SubtaskRepository>();

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

        }
    }
}
