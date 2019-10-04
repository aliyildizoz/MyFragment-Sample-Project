using MyFragment.Business.Abstract;
using MyFragment.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFragment.Business.Manager
{
    public class DirectorManager : ManagerBase<Director>, IManager
    {
        private static DirectorManager _directorManager;
        private DirectorManager()
        {

        }
        public static DirectorManager CreateAsSingleton()
        {
            if (_directorManager == null)
            {
                _directorManager = new DirectorManager();
            }
            return _directorManager;
        }

        public override int Insert(Director entity)
        {
            base.Insert(entity);
            Director director = Find(I => I.Name == entity.Name && I.Surname == entity.Surname);
            director.ImagePath = $"director_{director.Id.ToString()}";
            return Save();
        }
    }
}
