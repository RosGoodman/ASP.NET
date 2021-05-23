using System;
using System.Collections.Generic;

namespace MetricsManager.Repositories
{
    public interface IRepository<T> where T : class
    {
        IList<T> GetAll();
        T GetByRecordNumb(long id, long numb);
        void Create(T item);
        DateTimeOffset GetLastTime(long agentId);
    }
}
