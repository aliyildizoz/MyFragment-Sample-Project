using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFragment.Business.Abstract
{
    public class ManagerSingleton
    {
        private ManagerSingleton()
        {

        }
        public static T CreateAsSingleton<T>() where T : IManager
        {
            return (T)typeof(T).GetMethod("CreateAsSingleton").Invoke(null, null);

        }
    }
}
