using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DataAccessLayer.Contexts;
using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;

namespace TimeMate
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
            services.AddControllersWithViews();

            services.AddTransient<AccountDTO>();
            services.AddTransient<AgendaDTO>();
            services.AddTransient<AppointmentDTO>();
            services.AddTransient<JobDTO>();

            services.AddTransient<IAccountContext, SQLAccountContext>();
            services.AddTransient<IAgendaContext, SQLAgendaContext>();
            services.AddTransient<IAppointmentContext, SQLAppointmentContext>();
            services.AddTransient<IChecklistAppointmentContext, SQLChecklistAppointmentContext>();
            services.AddTransient<IJobContext, SQLJobContext>();
            services.AddTransient<INormalAppointmentContext, SQLNormalAppointmentContext>();

            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = false;
                options.Cookie.IsEssential = true;
                options.Cookie.Name = "Empyrean";
            });
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
                //app.UseExceptionHandler("/Home/Error");
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("Error/{0}");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSession();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Index}/{id?}");
            });
        }
    }
}
