using Microsoft.AspNetCore.JsonPatch;
using ToDoApp.Application.ToDos.Requests;
using ToDoApp.Application.ToDos.Responses;

namespace ToDoApp.Application.ToDos
{
    public interface IToDoService
    {
        Task<List<ToDoResponseModel>> GetAllAsync(CancellationToken cancellationToken,int userId,int status);
        Task<ToDoResponseModel> GetAsync(CancellationToken cancellationToken, int id, int userId);
        Task<ToDoResponseModel> CreateAsync(CancellationToken cancellationToken, ToDoRequestModel toDoRequest, int userId);
        Task<ToDoResponseModel> UpdateAsync(CancellationToken cancellationToken, ToDoUpdateModel person,int id, int userId);
        Task<ToDoResponseModel> UpdateToDoStatusAsync(CancellationToken cancellationToken, int id, int userId);
        Task<ToDoResponseModel> PatchAsync(CancellationToken cancellationToken, JsonPatchDocument<ToDoUpdateModel> patchRequest,int id, int userId);
        Task DeleteAsync(CancellationToken cancellationToken, int id, int userId);
    }
}