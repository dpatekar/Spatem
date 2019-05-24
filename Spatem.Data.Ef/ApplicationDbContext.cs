using Microsoft.EntityFrameworkCore;

namespace Spatem.Data.Ef
{
    internal class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}