using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebApp;
using WebApp.Entities;
using WebApp.Policies;
using WebApp.Services;

namespace WebApp
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services
                .AddEntityFrameworkSqlServer()
                .AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc().AddViewOptions(o =>
            {
                o.HtmlHelperOptions.ClientValidationEnabled = true;
            });

           services.AddAuthorization(config =>
          {
              Action<AuthorizationPolicyBuilder, string> employeeRoles = (p, roleName) =>
              {
                  p.AddRequirements(new EmployeePermissionRequired(roleName));
              };

              config.AddPolicy(AuthPolicy.Employees, p => { p.AddRequirements(new EmployeePermissionRequired()); });

              config.AddPolicy(AuthPolicy.ManageUser, p => { employeeRoles(p, RoleNames.ManageUser); });
              config.AddPolicy(AuthPolicy.CreateHousing, p => { employeeRoles(p, RoleNames.CreateHousing); });
              config.AddPolicy(AuthPolicy.EditHousing, p => { employeeRoles(p, RoleNames.EditHousing); });
              config.AddPolicy(AuthPolicy.DeleteHousing, p => { employeeRoles(p, RoleNames.DeleteHousing); });
              config.AddPolicy(AuthPolicy.CreateCustomer, p => { employeeRoles(p, RoleNames.CreateCustomer); });
              config.AddPolicy(AuthPolicy.EditCustomer, p => { employeeRoles(p, RoleNames.EditCustomer); });
              config.AddPolicy(AuthPolicy.DeleteCustomer, p => { employeeRoles(p, RoleNames.DeleteCustomer); });
          });

            // Add application services.
            // services.AddTransient<IEmailSender, AuthMessageSender>();
            // services.AddTransient<ISmsSender, AuthMessageSender>();

            services.AddScoped<AddressService>();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.CookieName = ".RealEstateCrmApplication";
            });

            services.AddMemoryCache();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseIdentity();

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
