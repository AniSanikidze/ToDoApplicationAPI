﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application;
using ToDo.Infrastructure;
using ToDo.Persistence.Context;
using ToDoApp.Application;

namespace ToDoApp.Infrastructure
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        #region DbContext And DbSets
        protected readonly ToDoContext _toDoContext;
        protected readonly DbSet<T> _dbSet;
        #endregion     
        #region Ctor
        public BaseRepository(ToDoContext toDoContext)
        {
            _toDoContext = toDoContext;
            _dbSet = _toDoContext.Set<T>();
        }
        #endregion

        #region Methods

        public async Task<T> GetAsync(CancellationToken token, Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.SingleOrDefaultAsync(predicate,token);
        }
        public async Task<T> AddAsync(CancellationToken token, T entity)
        {
            await _dbSet.AddAsync(entity,token);
            await _toDoContext.SaveChangesAsync(token);
            return entity;
        }
        public async Task<T> UpdateAsync(CancellationToken token, T entity)
        {
            _dbSet.Update(entity);
            await _toDoContext.SaveChangesAsync(token);
            //await _contextWrapper.SaveChanges(token, entity);
            return await GetAsync(token, x => x.Id == entity.Id);
        } 
        public async Task DeleteAsync(CancellationToken token, int id)
        {
            var entity = await GetAsync(token, x => x.Id == id);
            entity.Status = EntityStatuses.Deleted;
            _toDoContext.Entry(entity).State = EntityState.Modified;
            await _toDoContext.SaveChangesAsync(token);
        }
        public Task<bool> AnyAsync(CancellationToken token, Expression<Func<T, bool>> predicate)
        {
            return _dbSet.AnyAsync(predicate, token);
        }

        #endregion


    }
}
