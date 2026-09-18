using Gyarkorlas1.Models;
using Microsoft.EntityFrameworkCore;

namespace Gyarkorlas1.Data
{
    public class KonyvtarDBContext : DbContext
    {
        public KonyvtarDBContext(DbContextOptions<KonyvtarDBContext> options)
            :base(options)
        {
            
        }
        public DbSet<Konyv> konyvek { get; set; }
    }
}
