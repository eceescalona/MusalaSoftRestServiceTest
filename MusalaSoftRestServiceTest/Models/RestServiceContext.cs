using Microsoft.EntityFrameworkCore;

namespace MusalaSoftRestServiceTest.Models
{
    public class RestServiceContext : DbContext
    {
        public RestServiceContext(DbContextOptions<RestServiceContext> options)
            : base(options)
        {
        }

        public DbSet<Peripheral> Peripherals { get; set; }
        public DbSet<Gateway> Gateways { get; set; }
    }
}
