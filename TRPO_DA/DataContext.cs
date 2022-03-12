using Microsoft.EntityFrameworkCore;
using TRPO_DM.Models;

namespace TRPO_DA
{
    public class DataContext : DbContext
    {
        public DbSet<Element> Elements { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DataContext()
        {
            Database.EnsureCreated();
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=TRPODB;Trusted_Connection=True;");
        }
    }
}
