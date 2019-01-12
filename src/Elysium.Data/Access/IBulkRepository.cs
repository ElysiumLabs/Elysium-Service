using Elysium.Data.Entities;
using System.Collections.Generic;

namespace Elysium.Data.Access
{
    public interface IBulkRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        IEnumerable<TEntity> AddBulk(IEnumerable<TEntity> entities);

        IEnumerable<TEntity> DeleteBulk(IEnumerable<TEntity> entities);
    }
}