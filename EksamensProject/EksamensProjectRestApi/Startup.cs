using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EksamensProject.Core.ApplicationService;
using EksamensProject.Core.ApplicationService.Implementation;
using EksamensProject.Core.DomainService;
using EksamensProject.Core.Entity;
using EksamensProject.Core.Entity.Filters;
using EksamensProject.Infrastructure.SQL;
using EksamensProject.Infrastructure.SQL.Repositories;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace EksamensProjectRestApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Enviroment = env;
        }
        
        public IWebHostEnvironment Enviroment { get; set; }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // User injected
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            
            // Composition injected
            services.AddScoped<ICompositionRepository, CompositionRepository>();
            services.AddScoped<ICompositionService, CompositionService>();
            
            // Review injected
            services.AddScoped<ITestimonialRepository, TestimonialRepository>();
            services.AddScoped<ITestimonialService, TestimonialService>();
            
            // Request injected
            services.AddScoped<IRequestRepository, RequestRepository>();
            services.AddScoped<IRequestService, RequestService>();
            
            // DbInitialized 
            services.AddTransient<IDbInitializer, DbInitializer>();

            
            services.AddMvc(opt =>
                {
                    opt.Filters.Add<ValidationFilter>();
                })
                .AddFluentValidation(mvcConfig =>
                {
                    mvcConfig.RegisterValidatorsFromAssemblyContaining<UserValidator>();
                    mvcConfig.RegisterValidatorsFromAssemblyContaining<CompositionValidator>();
                    mvcConfig.RegisterValidatorsFromAssemblyContaining<RequestValidator>();
                    mvcConfig.RegisterValidatorsFromAssemblyContaining<ReviewValidator>();

                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            
            if (Enviroment.IsDevelopment())
            {
                services.AddDbContext<EksamensProjectContext>(
                    opt => opt.UseSqlite("Data Source=eksamensProject.db"));
            }
            else
            {
                services.AddDbContext<EksamensProjectContext>(opt =>
                    opt.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (Enviroment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    /*
                    var ctx = scope.ServiceProvider.GetRequiredService<EksamensProjectContext>();
                    var dbInitializer = ctx.GetService<IDbInitializer>();
                    dbInitializer.Initialize(ctx);
                    */
                    
                    var services = scope.ServiceProvider;
                    var dbContext = services.GetService<EksamensProjectContext>();
                    var dbInitializer = services.GetService<IDbInitializer>();
                    dbInitializer.Initialize(dbContext);
                    
                    
                }
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            //app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}