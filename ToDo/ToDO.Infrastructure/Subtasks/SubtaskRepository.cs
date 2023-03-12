using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application;
using ToDo.Domain.Subtasks;
using ToDo.Domain.ToDos;
using ToDo.Infrastructure;
using ToDo.Persistence.Context;
using ToDoApp.Application.Subtasks.Repositories;

namespace ToDoApp.Infrastructure.Subtasks
{
    public class SubtaskRepository : ISubtaskRepository
    {
        #region Private repository(HAS A)
        private IBaseRepository<Subtask> _repository;
        #endregion
        readonly ToDoContext _todoContext;

        #region Ctor
        public SubtaskRepository(IBaseRepository<Subtask> repository, ToDoContext toDoContext)
        {
            _repository = repository;
            _todoContext = toDoContext;
        }
        #endregion
        public async Task<Subtask> CreateAsync(CancellationToken cancellationToken, Subtask subtask)
        {
            await _repository.AddAsync(cancellationToken, subtask);
            return subtask;
        }

        public async Task DeleteAsync(CancellationToken cancellationToken, int id)
        {
            await _repository.DeleteAsync(cancellationToken, id);
        }

        public async Task<bool> Exists(CancellationToken cancellationToken, int id, int userId)
        {
            return await _repository.AnyAsync(cancellationToken, x => x.Id == id &&
                x.Status != EntityStatuses.Deleted && x.ToDo.OwnerId == userId);
        }

        public async Task<List<Subtask>> GetAllAsync(CancellationToken cancellationToken, int ownerId)
        {
            return await _todoContext.Subtasks.Where(x => x.ToDo.OwnerId == ownerId && x.Status == EntityStatuses.Active).ToListAsync(cancellationToken);
        }

        public async Task<Subtask> GetAsync(CancellationToken cancellationToken, int id, int ownerId)
        {
            return await _repository.GetAsync(cancellationToken,x => x.Id == id &&
                x.Status != EntityStatuses.Deleted && x.ToDo.OwnerId == ownerId);
        }

        public async Task<Subtask> UpdateAsync(CancellationToken cancellationToken, Subtask subtask, int ownerId)
        {
            var retrievedSubtask = await GetAsync(cancellationToken,subtask.Id,ownerId);
            _todoContext.Entry(retrievedSubtask).State = EntityState.Detached;
            subtask.CreatedAt = retrievedSubtask.CreatedAt;
            return await _repository.UpdateAsync(cancellationToken, subtask);
        }

    }
}
