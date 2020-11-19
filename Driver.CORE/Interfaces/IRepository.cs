using Driver.CORE.Entities;
using System;
using System.Collections.Generic;

namespace Driver.CORE.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        T GetById(Guid id);
        T GetSingleBySpec(ISpecification<T> spec);
        IEnumerable<T> ListAll();
        IEnumerable<T> List(ISpecification<T> spec);
        T Add(T entity);
        IEnumerable<T> AddAll(IEnumerable<T> entities);

        void Update(T entity);
        void Delete(T entity);
        void DeleteAll(List<T> entities);
        bool Exists(ISpecification<T> spec);
        T Clone(Guid id);
    }
}
