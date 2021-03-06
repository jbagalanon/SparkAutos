using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SparkAuto.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SparkAuto.Email;

namespace SparkAuto
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

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
                services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            //previous information is  services.AddDefaultIdentity<IdentityUser>()
                 services.AddIdentity<IdentityUser, IdentityRole>()
                     .AddDefaultTokenProviders()
                     .AddDefaultUI()
                .AddEntityFrameworkStores<ApplicationDbContext>();
                services.AddRazorPages().AddRazorRuntimeCompilation();
                services.AddSingleton<IEmailSender, EmailSender>();
                services.Configure<EmailOptions>(Configuration);
                services.AddAuthentication().AddFacebook(fb =>
                {
                    fb.AppId = "195405251711886";
                    fb.AppSecret = "c5e737b9771a3fd9d0acdc052299d9d9";
                });





                //services.AddAuthentication().AddFacebook(facebookOptions =>
                //{
                //    facebookOptions.AppId = Configuration["1954052517118860"];
                //    facebookOptions.AppSecret = Configuration["c5e737b9771a3fd9d0acdc052299d9d9"];
                //});


                //old fb API




                //companion of app.UseMvc() this code is no longer needed in latest version 

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
            //this code is exist at the 2.2 Core version app.UseMvc();
        }
    }
}
