using Elysium.Data.Entities;
using System.Threading.Tasks;

namespace Elysium.Data.Access
{
    public interface IRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : class, IEntity
    {
        void Create(TEntity entity);

        Task CreateAsync(TEntity entity);

        void Update(TEntity entity);

        Task UpdateAsync(TEntity entity);

        void Delete(TEntity entity);

        Task DeleteAsync(TEntity entity);

        void Delete(object id);

        Task DeleteAsync(object id);

        void Save();

        Task SaveAsync();
    }
}