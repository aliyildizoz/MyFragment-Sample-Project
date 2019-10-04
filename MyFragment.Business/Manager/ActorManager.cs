using MyFragment.Business.Abstract;
using MyFragment.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFragment.Business.Manager
{
    public class ActorManager : ManagerBase<Actor>, IManager
    {
        private static ActorManager actorManager;
        private ActorManager()
        {

        }
        public static ActorManager CreateAsSingleton()
        {
            if (actorManager == null)
            {
                actorManager = new ActorManager();
            }
            return actorManager;
        }

        public override int Insert(Actor entity)
        {
            base.Insert(entity);
            Actor actor = Find(I => I.Name == entity.Name && I.Surname == entity.Surname);
            actor.ImagePath = $"actor_{actor.Id.ToString()}";
            return Save();
        }
    }
}
