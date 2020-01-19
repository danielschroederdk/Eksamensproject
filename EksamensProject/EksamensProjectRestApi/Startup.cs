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
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
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
            // Create a byte array with random values. This byte array is used
            // to generate a key for signing JWT tokens.
            Byte[] secretBytes = new byte[40];
            Random rand = new Random();
            rand.NextBytes(secretBytes);

            // Add JWT based authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    //ValidAudience = "TodoApiClient",
                    ValidateIssuer = false,
                    //ValidIssuer = "TodoApi",
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretBytes),
                    ValidateLifetime = true, //validate the expiration and not before values in the token
                    ClockSkew = TimeSpan.FromMinutes(5) //5 minute tolerance for the expiration date
                };
            });
            services.AddControllers().AddNewtonsoftJson();
            
            // User injected
            services.AddScoped<IUserRepository<User>, UserRepository>();
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
            
            // Style injected
            services.AddScoped<IStylesRepository, StylesRepository>();
            services.AddScoped<IStyleService, StyleService>();
            
            // DbInitialized 
            services.AddTransient<IDbInitializer, DbInitializer>();
            // AuthService injected
            services.AddSingleton<IAuthenticationService>(new AuthenticationService(secretBytes));

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder
                        .WithOrigins("https://eksamensprojekt-379f8.firebaseapp.com").AllowAnyHeader().AllowAnyMethod()
                        .WithOrigins("https://eksamensproject-admin.firebaseapp.com").AllowAnyHeader().AllowAnyMethod()
                );
            });
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

            

            // Limiting reference loop
            services.AddMvc().AddNewtonsoftJson(opt =>
            {
                opt.SerializerSettings.MaxDepth = 3;
                opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            
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
            app.UseCors("AllowSpecificOrigin");

            //if (Enviroment.IsDevelopment())
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

            app.UseHsts();
            app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}