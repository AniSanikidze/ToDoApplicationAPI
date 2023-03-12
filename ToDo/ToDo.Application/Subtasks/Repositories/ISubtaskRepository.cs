using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Domain.Subtasks;
using ToDo.Domain.ToDos;

namespace ToDoApp.Application.Subtasks.Repositories
{
    public interface ISubtaskRepository
    {
        Task<List<Subtask>> GetAllAsync(CancellationToken cancellationToken, int ownerId);
        Task<Subtask> GetAsync(CancellationToken cancellationToken, int id, int ownerId);
        Task<Subtask> CreateAsync(CancellationToken cancellationToken, Subtask subtask);
        Task<Subtask> UpdateAsync(CancellationToken cancellationToken, Subtask subtask,int ownerId);
        Task DeleteAsync(CancellationToken cancellationToken, int id);
        Task<bool> Exists(CancellationToken cancellationToken, int id, int userId);
    }
}
