using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CMS.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CMS.Services;
using CMS.Models;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace CMS
{
    public class Startup
    {
        private UrlConstraint cmsUrlConstraint;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddScoped<ICMSService, CMSService>();

            services.AddDbContext<CMSContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection"))
                );

            //services.AddDefaultIdentity<User>()
            //    .AddEntityFrameworkStores<CMSContext>()
            //    .AddDefaultTokenProviders()
            //    .AddDefaultUI(UIFramework.Bootstrap4);

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<CMSContext>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddDefaultTokenProviders();

            services.AddScoped<IUserClaimsPrincipalFactory<User>, AppClaimsPrincipalFactory>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy(MyIdentityDataService.BlogPolicy_Add, policy => policy.RequireRole(MyIdentityDataService.AdminRoleName, MyIdentityDataService.EditorRoleName, MyIdentityDataService.ContributorRoleName));
                options.AddPolicy(MyIdentityDataService.BlogPolicy_Edit, policy => policy.RequireRole(MyIdentityDataService.AdminRoleName, MyIdentityDataService.EditorRoleName));
                options.AddPolicy(MyIdentityDataService.BlogPolicy_Delete, policy => policy.RequireRole(MyIdentityDataService.AdminRoleName));
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
                                IHostingEnvironment env,
                                IConfiguration configuration, 
                                UserManager<User> userManager,
                                RoleManager<IdentityRole> roleManager)
        {
            cmsUrlConstraint = new UrlConstraint(configuration);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();
            MyIdentityDataService.SeedData(userManager, roleManager);


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "CmsRoute",
                    template: "{*permalink}",
                    defaults: new { controller = "Content", action = "Index" },
                    constraints: new { permalink = cmsUrlConstraint }
                );

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }

    public class AppClaimsPrincipalFactory : UserClaimsPrincipalFactory<User, IdentityRole>
    {
        public AppClaimsPrincipalFactory(
            UserManager<User> userManager
            , RoleManager<IdentityRole> roleManager
            , IOptions<IdentityOptions> optionsAccessor)
        : base(userManager, roleManager, optionsAccessor)
        { }

        public async override Task<ClaimsPrincipal> CreateAsync(User user)
        {
            var principal = await base.CreateAsync(user);

    //        if (!string.IsNullOrWhiteSpace(user.Bio))
    //        {
    //            ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
    //    new Claim(ClaimTypes.Gender, user.Bio)
    //});
    //        }

    //        if (!string.IsNullOrWhiteSpace(user.Name))
    //        {
    //            ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
    //     new Claim(ClaimTypes.Name, user.Name),
    //});
    //        }

            return principal;
        }
    }
}
