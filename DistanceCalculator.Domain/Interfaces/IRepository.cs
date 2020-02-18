using System.Collections.Generic;

namespace DistanceCalculator.Domain.Interfaces
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetList();
        T Get(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        void Save();
    }
}