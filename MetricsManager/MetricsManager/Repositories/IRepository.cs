﻿using System.Collections.Generic;

namespace MetricsManager.Repositories
{
    public interface IRepository<T> where T : class
    {
        IList<T> GetAll();
        T GetById(int id);
        void Create(T item);
    }
}
