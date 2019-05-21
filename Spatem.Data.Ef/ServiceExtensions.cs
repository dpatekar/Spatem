using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Spatem.Data.Ef
{
    public static class ServiceExtensions
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
    }
}