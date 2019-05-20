using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Spatem.Data.Ef
{
    public static class SetupServices
    {
        public static void AddDataContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(connectionString,
               sqlServerOptions => sqlServerOptions.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName)));
        }

        public static IdentityBuilder AddIdentityDataContext(this IdentityBuilder identityBuilder)
        {
            return identityBuilder.AddEntityFrameworkStores<ApplicationDbContext>();
        }

        public static IIdentityServerBuilder AddConfigurationStore(this IIdentityServerBuilder identityServerBuilder, string connectionString)
        {
            return identityServerBuilder.AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = builder =>
                    builder.UseSqlServer(connectionString,
                        sqlServerOptions => sqlServerOptions.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName));
            });
        }

        public static IIdentityServerBuilder AddOperationalStore(this IIdentityServerBuilder identityServerBuilder, string connectionString)
        {
            return identityServerBuilder.AddOperationalStore(options =>
            {
                options.ConfigureDbContext = builder =>
                    builder.UseSqlServer(connectionString,
                        sqlServerOptions => sqlServerOptions.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName));
            });
        }
    }
}