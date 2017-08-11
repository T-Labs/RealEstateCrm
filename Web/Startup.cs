using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Data;
using Data.Command;
using Data.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Web.Helpers;
using Web.Models;
using WebApp;
using WebApp.Data;
using WebApp.Entities;
using WebApp.Models;
using WebApp.Policies;
using WebApp.Services;

namespace Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see https://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc().AddViewOptions(options =>
            {
                options.HtmlHelperOptions.ClientValidationEnabled = true;
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
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            services.AddScoped<AddressService>();
            services.AddScoped<ReadOnlyDataContext>();

            //services.AddScoped<IQueryHandler<HousiongPagedHandler.HomePageQuery, PagedResults<Housing>>>(service => new HousiongPagedHandler());

            Assembly assembly = Assembly.Load(new AssemblyName("Data")); //GetEntryAssembly();

            foreach (TypeInfo ti in assembly.DefinedTypes)
            {
                if (ti.Namespace != null && ti.Namespace.StartsWith("Data.Query"))
                {
                    foreach (var intref in ti.ImplementedInterfaces)
                    {
                        if (intref.Namespace.StartsWith("Data.Query") && intref.GetGenericTypeDefinition() == typeof(IQueryHandler<,>))
                        {
                            services.AddScoped(intref, ti.AsType());
                        }
                    }
                }
            }

            foreach (TypeInfo ti in assembly.DefinedTypes)
            {
                if (ti.Namespace != null && ti.Namespace.StartsWith("Data.Command"))
                {
                    foreach (var intref in ti.ImplementedInterfaces)
                    {
                        if (intref.Namespace.StartsWith("Data") && intref.Name.StartsWith("ICommandHandler")
                            && intref.GetGenericTypeDefinition() == typeof(ICommandHandler<>))
                        {
                            services.AddScoped(intref, ti.AsType());
                        }
                    }
                    /*
                    var intref = ti.ImplementedInterfaces.FirstOrDefault();
                    if (intref != null && intref.Name.StartsWith("ICommand"))
                    {
                        services.AddScoped(intref, ti.AsType());
                    }*/
                }
            }

            assembly = Assembly.GetEntryAssembly();

            foreach (TypeInfo ti in assembly.DefinedTypes)
            {
                if (ti.Namespace != null && ti.Namespace.StartsWith("Web.Helpers"))
                {
                    if (ti.IsClass)
                    {
                        services.AddScoped(ti.AsType());
                    }
                }
            }
            
            

            services.AddScoped<IQueryDispatcher>(service => new QueryDispatcher(service));
            services.AddScoped<ICommandDispatcher>(service => new CommandDispatcher(service));
            services.AddSingleton(service => new BuilderFactory(service));

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

                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                    .CreateScope())
                {
                    var dbContext = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                    var userManager = serviceScope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
                    var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                    
                    InitTestData.RoleSync(roleManager).GetAwaiter().GetResult();
                    InitTestData.DatabaseInitData(dbContext, userManager, roleManager).GetAwaiter().GetResult();
                }
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


            app.UseStaticFiles();

            app.UseIdentity();

            // Add external authentication middleware below. To configure them please see https://go.microsoft.com/fwlink/?LinkID=532715
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }


    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceProvider _resolver;

        public QueryDispatcher(IServiceProvider resolver)
        {
            _resolver = resolver;
        }

        public Task<TResult> ExecuteAsync<TQuery, TResult>(TQuery query)
            where TQuery : IQuery<TResult>
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            var handler = _resolver.GetService<IQueryHandler<TQuery, TResult>>();

            if (handler == null)
            {
                throw new Exception("QueryHandlerNotFoundException " + typeof(TQuery).FullName);
            }

            var readContext= _resolver.GetService<ReadOnlyDataContext>();

            return handler.ExecuteAsync(readContext, query);
        }
    }

    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _resolver;

        public CommandDispatcher(IServiceProvider resolver)
        {
            _resolver = resolver;
        }

        public async Task ExecuteAsync<TCommand>(TCommand command)
            where TCommand : ICommand
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            var handler = _resolver.GetService<ICommandHandler<TCommand>>();

            if (handler == null)
            {
                throw new Exception("QueryHandlerNotFoundException " + typeof(TCommand).FullName);
            }

            var dbContext = _resolver.GetService<ApplicationDbContext>();

            await handler.ExecuteAsync(dbContext, command);
        }
    }

    public class BuilderFactory
    {
        private readonly IServiceProvider _resolver;
        public BuilderFactory(IServiceProvider resolver)
        {
            _resolver = resolver;
        }
        
        public T Create<T>()
        {
            var item = _resolver.GetService<T>();
            if (item == null || item.GetType().GetInterfaces().All(x => x.GetGenericTypeDefinition() != typeof(IModelBuilder<>)))
            {
                throw new Exception("It is not modeul builder type: " + item.GetType().FullName);
            }
            return item;
        }
    }
}
