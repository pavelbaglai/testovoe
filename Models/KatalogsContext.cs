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
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public KatalogsContext(DbContextOptions<KatalogsContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string adminRoleName = "admin";
            string userRoleName = "user";
            string moderatorRoleName = "moderator";

            string adminEmail = "admin@mail.ru";
            string adminPassword = "123456";
           
           

            string moderatorEmail = "admin244@mail.ru";
            string moderatorPassword = "229728";

            // добавляем роли
            Role adminRole = new Role { Id = 1, Name = adminRoleName };
            Role userRole = new Role { Id = 2, Name = userRoleName };
            Role moderatorRole = new Role { Id = 3, Name = moderatorRoleName };
            User adminUser = new User { Id = 1, Email = adminEmail, Password = adminPassword, RoleId = adminRole.Id };
            User moderatorUser = new User { Id = 3, Email = moderatorEmail, Password = moderatorPassword, RoleId = moderatorRole.Id };

            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole, moderatorRole });
            modelBuilder.Entity<User>().HasData(new User[] { adminUser, moderatorUser });
            base.OnModelCreating(modelBuilder);
        }
    }
}

