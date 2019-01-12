using System;
using System.Linq.Expressions;

namespace Elysium.Data.Access
{
    public interface ICountRepository<T> : IDisposable where T : class
    {
        long Count(Expression<Func<T, bool>> whereCondition);

        long Count();
    }
}