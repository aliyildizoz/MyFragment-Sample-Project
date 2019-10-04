using MyFragment.DataAccess.Repositories;
using MyFragment.Entities.Abstract;
using MyFragment.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyFragment.Business.Abstract
{
    public abstract class ManagerBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        public Repository<TEntity> repository = new Repository<TEntity>();

        public virtual TEntity Attach(TEntity entity)
        {
            return repository.Attach(entity);
        }

        public virtual int Delete(TEntity entity)
        {
            return repository.Delete(entity);
        }

        public virtual TEntity Find(Expression<Func<TEntity, bool>> expression)
        {
            return repository.Find(expression);
        }

        public virtual int Insert(TEntity entity)
        {
            return repository.Insert(entity);
        }

        public virtual List<TEntity> List()
        {
            return repository.List();
        }

        public virtual List<TEntity> List(Expression<Func<TEntity, bool>> expression)
        {
            return repository.List(expression);
        }

        public virtual IQueryable<TEntity> QueryableList()
        {
            return repository.QueryableList();
        }

        public virtual IQueryable<TEntity> QueryableList(Expression<Func<TEntity, bool>> expression)
        {
            return repository.QueryableList(expression);
        }

        public virtual int Save()
        {
            return repository.Save();
        }

        public virtual int Update(TEntity entity)
        {
            return repository.Update(entity);
        }
    }
}
