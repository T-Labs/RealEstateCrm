using System;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebApp.Services;
using WebApp.Models;
using WebApp.Policies;

namespace WebApp
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();

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
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                // For more details on creating database during deployment see http://go.microsoft.com/fwlink/?LinkID=615859
                try
                {
                    using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                        .CreateScope())
                    {
                        var dbContext = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                        dbContext.Database.Migrate();
                    }
                }
                catch { }
            }

            app.UseIISPlatformHandler(options => options.AuthenticationDescriptions.Clear());

            app.UseStaticFiles();

            app.UseIdentity();

            // To configure external authentication please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
