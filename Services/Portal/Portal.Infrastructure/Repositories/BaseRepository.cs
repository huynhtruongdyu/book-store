using Microsoft.EntityFrameworkCore;
using Portal.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Portal.Infrastructure.Repositories
{
    public interface IBaseRepository<T>
    {
        IQueryable<T> FindAll(bool trackChanges);

        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression,
        bool trackChanges);

        List<T> GetAll();

        T GetById(int id);

        void Create(T entity);

        void Update(T entity);

        void Delete(T entity);
    }

    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly BookDbContext Context;
        protected readonly DbSet<T> DbSet;

        public BaseRepository(
            BookDbContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            DbSet = Context.Set<T>();
        }

        public void Create(T entity) => DbSet.Add(entity);

        public void Delete(T entity) => DbSet.Remove(entity);

        public IQueryable<T> FindAll(bool trackChanges) => !trackChanges ? DbSet.AsNoTracking() : DbSet;

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
        {
            return !trackChanges ? DbSet.Where(expression).AsNoTracking() : DbSet.Where(expression);
        }

        public List<T> GetAll() => DbSet.ToList();

        public T GetById(int id) => DbSet.Find(id);

        public void Update(T entity) => DbSet.Update(entity);
    }
}