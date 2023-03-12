using ToDoApp.Application.Subtasks.Requests;
using ToDoApp.Application.Subtasks.Responses;
using ToDoApp.Application.ToDos.Requests;
using ToDoApp.Application.ToDos.Responses;

namespace ToDoApp.Application.Subtasks
{
    public interface ISubtaskService
    {
        Task<List<SubtaskResponseModel>> GetAllAsync(CancellationToken cancellationToken, int userId);
        Task<SubtaskResponseModel> GetAsync(CancellationToken cancellationToken, int id, int userId);
        Task<SubtaskResponseModel> CreateAsync(CancellationToken cancellationToken, SubtaskRequestModel subtaskRequest, int userId);
        Task<SubtaskResponseModel> UpdateAsync(CancellationToken cancellationToken, SubtaskRequestModel subtaskRequest, int id, int userId);
        Task DeleteAsync(CancellationToken cancellationToken, int id, int userId);
    }
}