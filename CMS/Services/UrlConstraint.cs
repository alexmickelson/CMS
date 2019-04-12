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
    public class UrlConstraint : IRouteConstraint
    {
        private IConfiguration _configuration;

        public UrlConstraint(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if(values[routeKey] != null)
            {
                var permalink = values[routeKey].ToString();

                var optionsBuilder = new DbContextOptionsBuilder<CMSContext>();
                optionsBuilder.UseSqlite(_configuration.GetConnectionString("DefaultConnection"));
                var dbContext = new CMSContext(optionsBuilder.Options);

                var page = dbContext.Pages.FirstOrDefault(p => p.Url == permalink);
                if(page != null)
                {
                    httpContext.Items["cmspage"] = page;
                    return true;
                }
            }
            return false;
        }
    }
}
