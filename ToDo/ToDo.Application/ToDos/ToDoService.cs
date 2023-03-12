using Mapster;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using ToDo.Application;
using ToDo.Domain.Subtasks;
using ToDo.Domain.ToDos;
using ToDo.Domain.Users;
using ToDoApp.Application.CustomExceptions;
using ToDoApp.Application.Subtasks.Repositories;
using ToDoApp.Application.ToDos.Repositories;
using ToDoApp.Application.ToDos.Requests;
using ToDoApp.Application.ToDos.Responses;

namespace ToDoApp.Application.ToDos
{
    public class ToDoService : IToDoService
    {
        readonly IToDoRepository _toDoRepository;
        readonly ISubtaskRepository _subtaskRepository;
        public ToDoService(IToDoRepository toDoRepository, ISubtaskRepository subtaskRepository)
        {
            _toDoRepository = toDoRepository;
            _subtaskRepository = subtaskRepository;
        }
        public async Task<ToDoResponseModel> CreateAsync(CancellationToken cancellationToken, ToDoRequestModel toDoRequest, int userid)
        {
            var toDo = toDoRequest.Adapt<ToDoEntity>();
            toDo.ToDoStatus = ToDoStatuses.Active;
            toDo.OwnerId = userid;
            var result = await _toDoRepository.CreateAsync(cancellationToken, toDo);
            return result.Adapt<ToDoResponseModel>();
        }

        public async Task DeleteAsync(CancellationToken cancellationToken, int id, int userId)
        {
            if (!await _toDoRepository.Exists(cancellationToken, id, userId))
            {
                throw new ItemNotFoundException("ToDo not found", nameof(ToDo));
            }
            await _toDoRepository.DeleteAsync(cancellationToken, id,userId);
        }

        public async Task<List<ToDoResponseModel>> GetAllAsync(CancellationToken cancellationToken, int userId, int status)
        {
            var toDos = await _toDoRepository.GetAllAsync(cancellationToken, status, userId);
            return toDos.Adapt<List<ToDoResponseModel>>();
        }

        public async Task<ToDoResponseModel> GetAsync(CancellationToken cancellationToken, int id, int userId)
        {
            if (!await _toDoRepository.Exists(cancellationToken, id, userId))
            {
                throw new ItemNotFoundException("ToDo not found", nameof(ToDo));
            }

            var toDo = await _toDoRepository.GetAsync(cancellationToken, id, userId);
            return toDo.Adapt<ToDoResponseModel>();
        }

        public async Task<ToDoResponseModel> PatchAsync(CancellationToken cancellationToken, JsonPatchDocument<ToDoUpdateModel> patchRequest, int id, int userId)
        {
            var todo = await GetAsync(cancellationToken, id, userId);

            if (todo == null)
                throw new ItemNotFoundException("ToDo not found", nameof(ToDo));

            var toDoRequestModel = todo.Adapt<ToDoUpdateModel>();
            try
            {
                patchRequest.ApplyTo(toDoRequestModel);
            }
            catch (Exception ex)
            {
                throw new BadRequestWhenUpdatingWithPatch("Invalid data was provided", nameof(ToDo));
            }

            return await UpdateAsync(cancellationToken, toDoRequestModel, id, userId);
        }

        public async Task<ToDoResponseModel> UpdateAsync(CancellationToken cancellationToken, ToDoUpdateModel todoRequest, int id, int userId)
        {
            if (!await _toDoRepository.Exists(cancellationToken, id, userId))
            {
                throw new ItemNotFoundException("ToDo not found", nameof(ToDo));
            }
            ToDoEntity toDo = todoRequest.Adapt<ToDoEntity>();
            toDo.Id = id;
            toDo.OwnerId = userId;
            if (toDo.Subtasks != null && toDo.Subtasks.Count > 0)
            {
                foreach (var subtask in toDo.Subtasks)
                {
                    var retrievedSubtask = await _subtaskRepository.GetAsync(cancellationToken, subtask.Id, toDo.OwnerId);
                    if (retrievedSubtask == null)
                    {
                        throw new ItemNotFoundException("Subtask not found", nameof(Subtask));
                    }
                    subtask.CreatedAt = retrievedSubtask.CreatedAt;

                }
            }
            await _toDoRepository.UpdateAsync(cancellationToken, toDo);
            var result = await GetAsync(cancellationToken, id, userId);
            return result.Adapt<ToDoResponseModel>();
        }

        public async Task<ToDoResponseModel> UpdateToDoStatusAsync(CancellationToken cancellationToken, int id, int userId)
        {
            if (!await _toDoRepository.Exists(cancellationToken, id, userId))
            {
                throw new ItemNotFoundException("ToDo not found", nameof(ToDo));
            }
            var updatedToDo = await _toDoRepository.UpdateToDoStatusAsync(cancellationToken, id, userId);
            return updatedToDo.Adapt<ToDoResponseModel>();
        }
    }
}
