using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSwag.AspNetCore;
using Spatem.Core.Identity;
using Spatem.Data.Ef;

namespace Spatem.Api
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDataContext(Configuration.GetConnectionString("SpatemConnection"));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddIdentityDataContext()
                .AddDefaultTokenProviders();

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddOperationalStore(Configuration.GetConnectionString("SpatemConnection"))
                .AddConfigurationStore(Configuration.GetConnectionString("SpatemConnection"))
                .AddAspNetIdentity<ApplicationUser>();

            services.AddSwaggerDocument(settings =>
            {
                settings.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "Spatem API";
                    document.Info.Description = "REST API";
                };
            });

            services.AddAuthentication();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUi3();
            }
            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
        }
    }
}