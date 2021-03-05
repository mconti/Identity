using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using conti.maurizio.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace conti.maurizio
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //stringa di connessione per SQLServer
            //string connectionString = Configuration.GetConnectionString("default");
            //services.AddDbContext<AppDBContext>(c => c.UseSqlServer(connectionString));
            
            //stringa di connessione per SQLite
            string cn = "Data Source=database.db";

            // Registrazione globale (DI) del dbcontext 
            // per creare automaticamente il db quando serve
            services.AddDbContext<DBContext>(options => options.UseSqlite(cn));

            //services.AddIdentity<IdentityUser, IdentityRole>()
            services.AddIdentity<IdentityUser, IdentityRole>(opt =>
            {
                opt.Password.RequiredLength = 7;
                opt.Password.RequireDigit = false;
                opt.Password.RequireUppercase = false;
                opt.User.RequireUniqueEmail = true;
                opt.SignIn.RequireConfirmedEmail = true;
            })
            .AddEntityFrameworkStores<DBContext>();
            
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
            });

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo { Title = "Code Maze API", Version = "v1" }); 
                s.SwaggerDoc("v2", new OpenApiInfo { Title = "Code Maze API", Version = "v2" }); });

            services.AddControllersWithViews();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseSwagger(); 
            app.UseSwaggerUI(s => { 
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "Code Maze API v1"); 
                s.SwaggerEndpoint("/swagger/v2/swagger.json", "Code Maze API v2"); 
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
