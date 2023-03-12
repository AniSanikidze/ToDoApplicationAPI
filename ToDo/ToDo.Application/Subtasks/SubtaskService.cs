using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Domain.Subtasks;
using ToDo.Domain.ToDos;
using ToDoApp.Application.CustomExceptions;
using ToDoApp.Application.Subtasks.Repositories;
using ToDoApp.Application.Subtasks.Requests;
using ToDoApp.Application.Subtasks.Responses;
using ToDoApp.Application.ToDos.Repositories;

namespace ToDoApp.Application.Subtasks
{
    public class SubtaskService : ISubtaskService
    {
        readonly ISubtaskRepository _subtaskRepository;
        readonly IToDoRepository _toDoRepository;
        public SubtaskService(ISubtaskRepository subtaskRepository, IToDoRepository todoRepository)
        {
            _subtaskRepository = subtaskRepository;
            _toDoRepository = todoRepository;
        }
        public async Task<SubtaskResponseModel> CreateAsync(CancellationToken cancellationToken, SubtaskRequestModel subtaskRequest, int userId)
        {
            if (!await _toDoRepository.Exists(cancellationToken, subtaskRequest.ToDoItemId, userId))
                throw new Exception("Corresponding ToDo not found");

            var subtask = subtaskRequest.Adapt<Subtask>();
            await _subtaskRepository.CreateAsync(cancellationToken, subtask);
            return subtask.Adapt<SubtaskResponseModel>();
        }

        public async Task DeleteAsync(CancellationToken cancellationToken, int id, int userId)
        {
            if (!await _subtaskRepository.Exists(cancellationToken, id, userId))
                throw new ItemNotFoundException("Subtask not found", nameof(Subtask));
            await _subtaskRepository.DeleteAsync(cancellationToken, id);
        }

        public async Task<List<SubtaskResponseModel>> GetAllAsync(CancellationToken cancellationToken, int userId)
        {

            var subtasks = await _subtaskRepository.GetAllAsync(cancellationToken, userId);
            return subtasks.Adapt<List<SubtaskResponseModel>>();
        }

        public async Task<SubtaskResponseModel> GetAsync(CancellationToken cancellationToken, int id, int userId)
        {
            if (!await _subtaskRepository.Exists(cancellationToken, id, userId))
                throw new ItemNotFoundException("Subtask not found", nameof(Subtask));
            var retrievedSubtask = await _subtaskRepository.GetAsync(cancellationToken, id, userId);
            return retrievedSubtask.Adapt<SubtaskResponseModel>();
        }

        public async Task<SubtaskResponseModel> UpdateAsync(CancellationToken cancellationToken, SubtaskRequestModel subtaskRequest, int id, int userId)
        {
            if (!await _subtaskRepository.Exists(cancellationToken, id, userId))
                throw new ItemNotFoundException("Subtask not found", nameof(Subtask));

            if (!await _toDoRepository.Exists(cancellationToken, subtaskRequest.ToDoItemId, userId))
                throw new ItemNotFoundException("Todo not found", nameof(ToDoEntity));
            var subtask = subtaskRequest.Adapt<Subtask>();
            subtask.Id = id;
            var updatedSubtask = await _subtaskRepository.UpdateAsync(cancellationToken, subtask,userId);
            return updatedSubtask.Adapt<SubtaskResponseModel>();    
        }
    }
}
