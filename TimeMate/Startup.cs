using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DataAccessLayer.Containers;
using Microsoft.AspNetCore.Http;
using DataAccessLayer.Repositories;
using Core.DTOs;
using Core.Repositories;
using Core.Services;

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
            services.AddControllersWithViews().AddNewtonsoftJson();

            // Create a single instance of the DTO's & interface implementations for the duration of the application lifetime.
            services.AddSingleton<AccountDTO>();
            services.AddSingleton<AgendaDTO>();
            services.AddSingleton<AppointmentDTO>();
            services.AddSingleton<JobDTO>();

            services.AddSingleton<IAccountRepository, SQLAccountRepository>();
            services.AddSingleton<IAgendaRepository, SQLAgendaRepository>();
            services.AddSingleton<IAppointmentRepository, SQLAppointmentRepository>();
            services.AddSingleton<IChecklistAppointmentRepository, SQLChecklistAppointmentRepository>();
            services.AddSingleton<IJobRepository, SQLJobRepository>();
            services.AddSingleton<INormalAppointmentRepository, SQLNormalAppointmentRepository>();
            services.AddSingleton<ISender, MailSenderService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = false;
                options.Cookie.IsEssential = true;
                options.Cookie.Name = "Account_Session";
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
                //app.UseExceptionHandler("/Home/Error"); // Default error controller.
                app.UseExceptionHandler("/Error"); // Custom error controller.
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
