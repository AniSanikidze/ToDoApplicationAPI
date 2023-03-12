using ToDoApp.Application.Users.Requests;
using ToDoApp.Application.Users.Responses;

namespace ToDoApp.Application.Users
{
    public interface IUserService
    {
        Task<UserResponseModel> AuthenticateAsync(CancellationToken cancellation,UserRequestModel user);
        Task<UserResponseModel> CreateAsync(CancellationToken cancellation, UserRequestModel user);
    }
}