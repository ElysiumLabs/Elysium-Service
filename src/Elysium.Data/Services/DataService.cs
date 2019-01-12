using Elysium.Data.Access;
using System;

namespace Elysium.Data.Services
{
    public class DataService
    {
        protected readonly IUnitOfWork unitOfWork;

        public DataService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public virtual bool Commit()
        {
            return unitOfWork.Commit() > 0;
        }

        public virtual bool CommitDo(Action ifCommitAction)
        {
            var commited = Commit();

            if (commited)
            {
                ifCommitAction?.Invoke();
            }

            return commited;
        }
    }
}