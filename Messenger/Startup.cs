using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Messenger.Lib;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repository.Data;
using Repository.Repositories.AccountRepository;
using Repository.Repositories.AuthRepositories;
using Repository.Repositories.ChatRepository;
using Repository.Repositories.SearchRepository;
using Repository.Repositories.SignalRepository;
using Repository.Services;
using Website.Hubs;

namespace Messenger
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
            services.AddAutoMapper(typeof(Startup));  // Auto Mapper Connection..
            services.AddDbContext<MessengerDbContext>(options => options.
                                                           UseSqlServer(Configuration.
                                                           GetConnectionString("Default"),
                                                           x => x.MigrationsAssembly("Repository")));


            services.AddTransient<IAuthRepository, AuthRepository>();  //AuthRepository Service Added
            services.AddTransient<IAccountDetailRepository, AccountDetailRepository>();  //AccountDetailRepository Service Added
            services.AddTransient<ICloudinaryService, CloudinaryService>();
            services.AddTransient<IFileManager, FileManager>();
            services.AddTransient<ISendEmail, SendEmail>();  //For send email service
            services.AddTransient<ISearchRepository, SearchRepository>();
            //services.AddTransient<INotificationRepository, NotificationRepository>();
            services.AddTransient<IFriendsRepository, FriendsRepository>();

            services.AddTransient<ISignalrRepository, SignalrRepository>();  //testing

            services.AddTransient<IChatRepository, ChatRepository>();  

            services.AddSignalR();

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
                app.UseExceptionHandler("/home/error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            //app.Accountouting();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=quickymessanger}/{action=index}/{id?}");

                //SignalR
                endpoints.MapHub<RealTimeHub>("/realTimeHub");
            });
        }
    }
}
