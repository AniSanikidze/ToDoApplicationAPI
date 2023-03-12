using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application;
using ToDo.Domain.ToDos;
using ToDo.Domain.Users;
using ToDo.Infrastructure;
using ToDo.Persistence.Context;
using ToDoApp.Application;
using ToDoApp.Application.Subtasks.Repositories;
using ToDoApp.Application.ToDos.Repositories;

namespace ToDoApp.Infrastructure.ToDos
{
    public class ToDoRepository : IToDoRepository
    {
        #region Private repository(HAS A)
        private IBaseRepository<ToDoEntity> _repository;
        #endregion
        readonly ToDoContext _todoContext;
        private readonly ISubtaskRepository _subtaskRepository;

        #region Ctor
        public ToDoRepository(IBaseRepository<ToDoEntity> repository, ToDoContext toDoContext, ISubtaskRepository subtaskRepository)
        {
            _repository = repository;
            _todoContext = toDoContext;
            _subtaskRepository = subtaskRepository;
        }
        #endregion

        #region Methods
        public async Task<List<ToDoEntity>> GetAllAsync(CancellationToken cancellationToken, int status, int ownerId)
        {
            ToDoStatuses filterStatus = ToDoStatuses.Active;
            if (status != 0)
            {
                switch (status)
                {
                    case 1:
                        filterStatus = ToDoStatuses.Active;
                        break;
                    case 2:
                        filterStatus = ToDoStatuses.Done;
                        break;
                    case 3:
                        throw new Exception("Cannot filter todos by deleted status");
                    default:
                        break;
                }
                return await _todoContext.ToDos.Include(x => x.Subtasks.Where(x => x.Status == EntityStatuses.Active)).Where(x => x.Status == EntityStatuses.Active && x.OwnerId == ownerId
                && x.ToDoStatus == filterStatus).ToListAsync(cancellationToken);
            }
            return await _todoContext.ToDos.Include(x => x.Subtasks.Where(x => x.Status == EntityStatuses.Active)).Where(x => x.Status == EntityStatuses.Active && x.OwnerId == ownerId)
                .ToListAsync(cancellationToken);
        }

        public async Task<ToDoEntity> GetAsync(CancellationToken cancellationToken, int id, int ownerId)
        {
            return await _todoContext.ToDos.Include(x => x.Subtasks.Where(x => x.Status == EntityStatuses.Active))
                .FirstOrDefaultAsync(x => x.Id == id && x.Status == EntityStatuses.Active, cancellationToken);
        }
        public async Task<ToDoEntity> CreateAsync(CancellationToken cancellationToken, ToDoEntity toDo)
        {
            return await _repository.AddAsync(cancellationToken, toDo);
        }

        public async Task DeleteAsync(CancellationToken cancellationToken, int id, int ownerId)
        {
            var toDo = await GetAsync(cancellationToken, id, ownerId);
            toDo.ModifiedAt = DateTime.Now;
            if (toDo.Subtasks != null && toDo.Subtasks.Count > 0)
            {
                toDo.Subtasks?.ForEach(x => x.Status = EntityStatuses.Deleted);
            }
            await _repository.DeleteAsync(cancellationToken, id);
        }

        public async Task<bool> Exists(CancellationToken cancellationToken, int id, int userId = -1)
        {
            return await _repository.AnyAsync(cancellationToken, x => x.Id == id &&
                x.Status != EntityStatuses.Deleted && (userId != -1 ? x.OwnerId == userId : true));
        }

        public async Task<ToDoEntity> UpdateAsync(CancellationToken cancellationToken, ToDoEntity toDo)
        {
            var retrievedTodo = await GetAsync(cancellationToken, toDo.Id, toDo.OwnerId);
            _todoContext.Entry(retrievedTodo).State = EntityState.Detached;
            toDo.CreatedAt = retrievedTodo.CreatedAt;
            toDo.ToDoStatus = retrievedTodo.ToDoStatus;
            return await _repository.UpdateAsync(cancellationToken, toDo);
        }

        public async Task<ToDoEntity> UpdateToDoStatusAsync(CancellationToken cancellationToken, int id, int ownerId)
        {
            var retrievedTodo = await GetAsync(cancellationToken, id, ownerId);
            retrievedTodo.ToDoStatus = ToDoStatuses.Done;
            return await _repository.UpdateAsync(cancellationToken, retrievedTodo);
        }
        #endregion
    }
}
