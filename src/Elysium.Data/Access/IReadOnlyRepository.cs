///Inspired in https://cpratt.co/truly-generic-repository/
using Elysium.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Elysium.Data.Access
{
    public interface IReadOnlyRepository<TEntity> where TEntity : class, IEntity
    {
        IEnumerable<TEntity> GetAll(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Expression<Func<TEntity, object>>[] includes = null,
            int? skip = null,
            int? take = null);

        Task<IEnumerable<TEntity>> GetAllAsync(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Expression<Func<TEntity, object>>[] includes = null,
            int? skip = null,
            int? take = null);

        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Expression<Func<TEntity, object>>[] includes = null,
            int? skip = null,
            int? take = null);

        Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Expression<Func<TEntity, object>>[] includes = null,
            int? skip = null,
            int? take = null);

        TEntity GetById(
            object id,
            Expression<Func<TEntity, object>>[] includes = null);

        Task<TEntity> GetByIdAsync(
            object id,
            Expression<Func<TEntity, object>>[] includes = null);

        int GetCount(Expression<Func<TEntity, bool>> filter = null);

        Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter = null);

        bool GetExists(Expression<Func<TEntity, bool>> filter = null);

        Task<bool> GetExistsAsync(Expression<Func<TEntity, bool>> filter = null);
    }

    //public interface IRepository<T> : IDisposable
    //{
    //    T Create(T entity);

    // T Update(T entity);

    // T Delete(T entity);

    // T Find(params object[] keyvalues);

    // T Get(Expression<Func<T, bool>> whereCondition);

    //    IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes);
    //}
}