using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Domain.Users;

namespace ToDoApp.Application.Users.Repositores
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(CancellationToken cancellationToken, User user);

        Task<User> GetAsync(CancellationToken cancellationToken, User user);

        Task<bool> Exists(CancellationToken cancellationToken, int userId);

        //Task<User> GetByTokenAsync(CancellationToken cancellationToken, string token);
    }
}
