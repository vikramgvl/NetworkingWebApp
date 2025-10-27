using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NetworkingWebApp.Entities;

namespace NetworkingWebApp.Data
{
   
        public class AppDbContext(DbContextOptions options):DbContext(options)
        {
            public DbSet<AppUser> Users { get; set; }
        }
    
}
