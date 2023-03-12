using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Domain.ToDos;

namespace ToDoApp.Application.ToDos.Repositories
{
    public interface IToDoRepository
    {
        Task<List<ToDoEntity>> GetAllAsync(CancellationToken cancellationToken,int status, int ownerId);
        Task<ToDoEntity> GetAsync(CancellationToken cancellationToken, int id, int ownerId);
        Task<ToDoEntity> CreateAsync(CancellationToken cancellationToken, ToDoEntity toDo);
        Task<ToDoEntity> UpdateAsync(CancellationToken cancellationToken, ToDoEntity toDo);
        Task<ToDoEntity> UpdateToDoStatusAsync(CancellationToken cancellationToken, int id, int ownerId);
        Task DeleteAsync(CancellationToken cancellationToken, int id, int ownerId);
        Task<bool> Exists (CancellationToken cancellationToken, int id, int userId);
    }
}
