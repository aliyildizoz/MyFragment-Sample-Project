using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFragment.Entities.Entity.Enums
{
    public enum ResultState
    {
        Success = 101,
        Error = 102,
        NotFound = 103,
        UsernameAlreadyExists = 104,
        EmailAlreadyExists = 105,
        UsernameEmailAlreadyExists = 106
    }
}
