using Driver.CORE.Entities;
using Driver.CORE.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Driver.INFRASTRUCTURE.Data
{
    public class Repository<T> : IRepository<T>, IAsyncRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _dbContext;

        public Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual T GetById(Guid id)
        {
            return _dbContext.Set<T>().Find(id);
        }
        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public T GetSingleBySpec(ISpecification<T> spec)
        {
            return List(spec).FirstOrDefault();
        }

        public Task<T> GetSingleBySpecAsync(ISpecification<T> spec)
        {
            // fetch a Queryable that includes all expression-based includes
            var queryableResultWithIncludes = spec.Includes
                .Aggregate(_dbContext.Set<T>().AsQueryable(),
                    (current, include) => current.Include(include));

            // modify the IQueryable to include any string-based include statements
            var secondaryResult = spec.IncludeStrings
                .Aggregate(queryableResultWithIncludes,
                    (current, include) => current.Include(include));

            // return the result of the query using the specification's criteria expression
            return secondaryResult.Where(spec.Criteria).FirstOrDefaultAsync();
        }

        public IEnumerable<T> ListAll()
        {
            return _dbContext.Set<T>().AsEnumerable();
        }
        public IEnumerable<T> List(ISpecification<T> spec)
        {
            // fetch a Queryable that includes all expression-based includes
            var queryableResultWithIncludes = spec.Includes
                .Aggregate(_dbContext.Set<T>().AsQueryable(),
                    (current, include) => current.Include(include));

            // modify the IQueryable to include any string-based include statements
            var secondaryResult = spec.IncludeStrings
                .Aggregate(queryableResultWithIncludes,
                    (current, include) => current.Include(include));

            // return the result of the query using the specification's criteria expression
            return secondaryResult
                .Where(spec.Criteria)
                .AsEnumerable();
        }

        public virtual IQueryable<T> SqlQuery(string query)
        {
            return _dbContext.Set<T>().FromSqlRaw(query);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }
        public async Task<IReadOnlyList<T>> ListAsyncBySpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public T Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();

            return entity;
        }

        public async Task<T> AddAsync(T entity)
        {
            entity.Id = Guid.NewGuid();
            entity.Inserted = DateTime.UtcNow;
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }


        public async Task<IEnumerable<T>> AddAllAsync(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().AddRange(entities);
            await _dbContext.SaveChangesAsync();
            return entities;
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();
        }
        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public void DeleteAll(List<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
            _dbContext.SaveChanges();
        }
        public async Task DeleteAllAsync(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
            await _dbContext.SaveChangesAsync();
        }

        public bool Exists(ISpecification<T> spec)
        {
            return List(spec).Any();
        }

        public T Clone(Guid id)
        {
            var settings = GetById(id);
            var shallowCopy = (T)_dbContext.Entry(settings).CurrentValues.ToObject();

            return shallowCopy;
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>(), spec);
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
            => _dbContext.Set<T>().Where(predicate);

        public IEnumerable<T> AddAll(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().AddRange(entities);
            _dbContext.SaveChanges();
            return entities;
        }


    }
}
