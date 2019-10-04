using MyFragment.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyFragment.UI.Filters
{
    public class AutAdmin : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (CurrentSession.user != null)
            {
                if (CurrentSession.user.UserState == Entities.Entity.Enums.UserState.StandartUser)
                {
                    filterContext.Result = new RedirectResult("/Home/AccessDenied");
                }
            }
            else
            {
                filterContext.Result = new RedirectResult("/Home/AccessDenied");
            }
        }
    }
}