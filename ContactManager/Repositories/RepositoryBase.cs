using ContactManager.Contexts;
using ContactManager.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace ContactManager.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected ContactsContext _contactsContext;
        public RepositoryBase(ContactsContext contactsContext)
        {
            _contactsContext = contactsContext;
        }

        public IQueryable<T> FindAll(bool trackChanges) =>
            !trackChanges ?
              _contactsContext.Set<T>()
                .AsNoTracking() :
              _contactsContext.Set<T>();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
            !trackChanges ?
              _contactsContext.Set<T>()
                .Where(expression)
                .AsNoTracking() :
              _contactsContext.Set<T>()
                .Where(expression);

        public void Create(T entity) => _contactsContext.Set<T>().Add(entity);

        public void Update(T entity) => _contactsContext.Set<T>().Update(entity);

        public void Delete(T entity) => _contactsContext.Set<T>().Remove(entity);
    }
}
