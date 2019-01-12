using System;

namespace Elysium.Data.Access
{
    public interface IUnitOfWork : IDisposable
    {
        int Commit();
    }
}