using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SportsClubModel;
using EF_DB_Layer;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using SportsClubModel.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Cors;

namespace SportsClubWeb
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        readonly string MyAllowSpecificOrigins = "_MyAllowSpecificOrigins";
 
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });


            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration["Data:SportsClub:ConnectionString"]));
            services.AddTransient<IChallengeRepository, EFChallengeRepository>();
            services.AddTransient<IFieldRepository, EFFieldRepository>();
            services.AddTransient<IReservationRepository, EFReservationRepository>();
            services.AddTransient<IUserRepository, EFUserRepository>();
            services.AddTransient<IChallengeUnitOfWork, EFChallengeUnitOfWork>();
            services.AddTransient<IFieldUnitOfWork, EFFieldUnitOfWork>();
            services.AddTransient<IReservationUnitOfWork, EFReservationUnitOfWork>();
            services.AddTransient<IUserUnitOfWork, EFUserUnitOfWork>();
            services.AddAutoMapper();
            /*services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();*/
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            /*services.AddMemoryCache();
            services.AddSession();*/

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(MyAllowSpecificOrigins);
            app.UseMvc();
        }
    }
}
