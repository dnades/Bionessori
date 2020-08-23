using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bionessori.Core;
using Bionessori.Core.Data;
using Bionessori.Core.Extensions;
using Bionessori.Core.Interfaces;
using Bionessori.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Bionessori {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            string connectionString = "Server=skyhorizen.ru,1433; Database=u0772479_pacidb; Persist Security Info=False; User ID=u0772479_admin; Password=K3sxb30*;MultipleActiveResultSets=False; Encrypt=True; TrustServerCertificate=true; Connection Timeout=30;Integrated Security=False;";

            services.AddTransient<IUserRepository, UserService>(provider => new UserService(connectionString));

            services.AddTransient<ICard, CardPatientService>(provider => new CardPatientService(connectionString));

            services.AddTransient<IBackOffice, BackOfficeService>(provider => new BackOfficeService(connectionString));

            services.AddTransient<IRegistry, RegistryService>(provider => new RegistryService(connectionString));

            services.AddTransient<IFrontOffice, FrontOfficeService>(provider => new FrontOfficeService(connectionString));            
            services.AddControllersWithViews();

            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(
                   Configuration.GetConnectionString("DefaultConnection")));

            services.AddCors(options => options.AddPolicy("ApiCorsPolicy", builder => {
                builder.WithOrigins("https://apihosting.online/", "https://apihosting.online").AllowAnyMethod().AllowAnyHeader();
            }));

            services.AddMvc(option => option.EnableEndpointRouting = false);

            services.AddControllers()
        .AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new IntToStringExtension()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            else {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Route}/{action=Index}/{id?}");
            });
        }
    }
}
