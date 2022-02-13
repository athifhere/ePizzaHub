using ePizzaHub.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ePizzaHub.DAL
{
    public class DatabaseContext : IdentityDbContext<User, Role, int>
    {
        public DatabaseContext()
        {

        }

        public DatabaseContext(DbContextOptionsBuilder options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-7OCUR3A;Initial Catalog=ePizzaHub03;Integrated Security=True");
            }
        }
    }
}