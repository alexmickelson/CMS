using CMS.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Services
{
    public class CmsUrlConstraint : IRouteConstraint
    {
        private readonly IConfiguration configuration;

        public CmsUrlConstraint(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public bool Match(HttpContext httpContext, IRouter route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (values[parameterName] != null)
            {
                var permalink = values[parameterName].ToString();
                var optionsBuilder = new DbContextOptionsBuilder<CMSContext>();
                optionsBuilder.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
                var db = new CMSContext(optionsBuilder.Options);
                var page = db.Pages.FirstOrDefault(p => p.Url == permalink);
                if (page != null)
                {
                    httpContext.Items["cmspage"] = page;
                    return true;
                }
            }
            
            return false;
        }
    }
}
