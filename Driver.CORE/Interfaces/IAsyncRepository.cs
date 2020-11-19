using Driver.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Driver.CORE.Interfaces
{
    public interface IAsyncRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(Guid id);
        T GetSingleBySpec(ISpecification<T> spec);
        Task<T> GetSingleBySpecAsync(ISpecification<T> spec);
        IEnumerable<T> ListAll();
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<IReadOnlyList<T>> ListAsyncBySpec(ISpecification<T> spec);
        IEnumerable<T> List(ISpecification<T> spec);
        T Add(T entity);
        Task<IEnumerable<T>> AddAllAsync(IEnumerable<T> entities);
        Task<T> AddAsync(T entity);
        void Update(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<int> CountAsync(ISpecification<T> spec);
        Task DeleteAllAsync(IEnumerable<T> entities);
    }
}
