using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.Routing;
using PhotoBooth.BL;
using PhotoBooth.BL.Facades;
using PhotoBooth.DAL;
using PhotoBooth.DAL.Entity;
using PhotoBooth.Mocks;

namespace PhotoBooth.WEB
{
    public class Startup
    {
        public IConfiguration Configuration { get; private set; }
        public Startup(IWebHostEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json");
            
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDataProtection();
            services.AddAuthorization();
            services.AddWebEncoders();
            Install(services);
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<PhotoBoothContext>()
                .AddDefaultTokenProviders();
			services.ConfigureApplicationCookie(o => { o.LoginPath = new PathString("/Authentication/SignIn"); });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Authentication/SignIn";
                });
            services.AddDotVVM<DotvvmStartup>();
        }

        private void Install(IServiceCollection services)
        {
            services.AddDbContext<PhotoBoothContext>(builder =>
            {

                builder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            } ,ServiceLifetime.Transient, ServiceLifetime.Singleton);

            BlInstaller.Install(services);
            DALInstaller.Install(services);
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseAuthentication();
            // use DotVVM
            var dotvvmConfiguration = app.UseDotVVM<DotvvmStartup>(env.ContentRootPath);
            dotvvmConfiguration.AssertConfigurationIsValid();
            
            // use static files
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(env.WebRootPath)
            });
        }
    }
}
