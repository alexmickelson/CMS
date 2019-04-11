using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Services
{
    public class CmsUrlConstraint
    {
        //public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        //{
        //    var db = new MvcCMS.Models.MvcCMSContext();
        //    if (values[parameterName] != null)
        //    {
        //        var permalink = values[parameterName].ToString();
        //        return db.CMSPages.Any(p => p.Permalink == permalink);
        //    }
        //    return false;
        //}
    }
}
