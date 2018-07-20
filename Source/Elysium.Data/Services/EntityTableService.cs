using Elysium.Data.Access;
using Elysium.Data.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Elysium.Data.Services
{
    public class EntityTableService<TModel, TEntity, TKey> : DataService
        where TModel : class, IEntity<TKey>
        where TEntity : class, IEntity<TKey>, IDataEntityModel<TModel>
        where TKey : IEquatable<TKey>
    {
        public bool UseSoftDelete { get; set; } = false;

        private readonly IRepository<TEntity> repository;

        public EntityTableService(
            IUnitOfWork unitOfWork,
            IRepository<TEntity> repository
            )
            : base(unitOfWork)
        {
            this.repository = repository;
        }

        public virtual TModel Create(TModel model)
        {
            var entity = Activator.CreateInstance<TEntity>();

            entity.CreateFrom(model);

            repository.Create(entity);

            return Commit()
                ? entity.ToModel()
                : null;
        }

        public virtual TModel Update(TKey key, TModel model)
        {
            var entity = repository.GetById(key);

            if (entity == null)
            {
                throw new Exception("Entity not found");
            }

            entity.UpdateFrom(model);

            repository.Update(entity);

            return Commit()
                ? entity.ToModel()
                : null;
        }

        public virtual TModel GetById(object key, params Expression<Func<TEntity, object>>[] includes)
        {
            var entity = repository.GetById(key, includes);

            if (entity == null)
            {
                throw new Exception("Entity not found");
            }

            return entity.ToModel();
        }

        public virtual TModel Delete(string id)
        {
            var entity = repository.GetById(id);

            if (entity == null)
            {
                throw new Exception("Entity not found");
            }

            entity.DeleteFrom(entity.ToModel(), UseSoftDelete);

            if (!UseSoftDelete)
            {
                repository.Delete(entity);
            }
            else
            {
                repository.Update(entity);
            }

            Commit();

            return null;
        }

        public virtual IQueryable<TModel> GetAll(Expression<Func<TEntity, object>>[] includes = null, int? skip = null, int? take = null)
        {
            return repository
                .GetAll(null, includes, skip, take)
                .Where(x => !x.Deleted)
                .ToList()
                //.ProjectTo<TModel>(); arrumar futuro
                .Select(x => x.ToModel())
                .AsQueryable();
        }
    }
}