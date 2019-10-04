using MyFragment.Entities.Entity;
using MyFragment.Entities.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFragment.Entities.ViewModel
{
    public class Result
    {
        public User User { get; set; }
        public ResultState ResultState { get; set; }
    }
}
