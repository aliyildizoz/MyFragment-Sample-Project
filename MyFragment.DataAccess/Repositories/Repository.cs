using MyFragment.DataAccess.EntityFramework;
using MyFragment.Entities.Abstract;
using MyFragment.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyFragment.DataAccess.Repositories
{
    public class Repository<T> : RepositoryBase, IRepositoryBase<T> where T : class
    {
       
        public DbSet<T> _objectSet;
        public Repository()
        {
            _objectSet = context.Set<T>();
        }

        public int Delete(T entity)
        {
            _objectSet.Remove(entity);
            return Save();
        }

        public T Find(Expression<Func<T, bool>> expression)
        {
            return _objectSet.FirstOrDefault(expression);
        }

        public int Insert(T entity)
        {
            _objectSet.Add(entity);
            return Save();
        }

        public List<T> List()
        {
            return _objectSet.ToList();
        }

        public List<T> List(Expression<Func<T, bool>> expression)
        {
            return _objectSet.Where(expression).ToList();
        }

        public IQueryable<T> QueryableList()
        {
            return _objectSet.AsQueryable();
        }

        public IQueryable<T> QueryableList(Expression<Func<T, bool>> expression)
        {
            return _objectSet.AsQueryable().Where(expression);
        }

        public int Save()
        {
            return context.SaveChanges();
        }

        public int Update(T entity)
        {
            return Save();
        }
        public T Attach(T entity)
        {
            return _objectSet.Attach(entity);
        }
    }
}
