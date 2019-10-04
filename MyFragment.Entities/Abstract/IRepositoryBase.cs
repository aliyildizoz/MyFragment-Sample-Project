using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyFragment.Entities.Abstract
{
    public interface IRepositoryBase<T> where T : class
    {
        List<T> List();
        List<T> List(Expression<Func<T, bool>> expression);
        IQueryable<T> QueryableList();
        IQueryable<T> QueryableList(Expression<Func<T, bool>> expression);
        T Find(Expression<Func<T, bool>> expression);
        int Update(T entity);
        int Delete(T entity);
        int Insert(T entity);
        int Save();
        T Attach(T entity);
    }
}
