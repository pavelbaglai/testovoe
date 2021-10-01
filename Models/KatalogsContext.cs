using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace WebApplication39.Models
{
    public class KatalogsContext : DbContext
    {
        public DbSet<Katalog> Katalogs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public KatalogsContext(DbContextOptions<KatalogsContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
    

