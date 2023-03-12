using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application;
using ToDo.Domain.Users;
using ToDo.Infrastructure;
using ToDo.Persistence.Context;
using ToDoApp.Application.Users.Repositores;

namespace ToDoApp.Infrastructure.Users
{
    public class UserRepository : IUserRepository
    {
        #region Private repository(HAS A)
        private IBaseRepository<User> _repository;
        #endregion
        readonly ToDoContext _todoContext;

        #region Ctor
        public UserRepository(IBaseRepository<User> repository, ToDoContext toDoContext)
        {
            _repository = repository;
            _todoContext = toDoContext;
        }
        #endregion
        public async Task<User> CreateAsync(CancellationToken cancellationToken, User user)
        {
            return await _repository.AddAsync(cancellationToken, user);
        }

        public async Task<bool> Exists(CancellationToken cancellationToken, int userId)
        {
            return await _repository.AnyAsync(cancellationToken, x => x.Id == userId &&
                x.Status != EntityStatuses.Deleted);
        }

        public async Task<User> GetAsync(CancellationToken cancellationToken, User user)
        {
            return await _todoContext.Users.SingleOrDefaultAsync(x => x.Username == user.Username && x.Password == user.Password &&
            x.Status == EntityStatuses.Active, cancellationToken);
        }
    }
}
