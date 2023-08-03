using Microsoft.EntityFrameworkCore;
using Project1.Models;
using Project1.Models.DTO;


namespace Project1.Database
{
  

    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }



        public DbSet<User> Users { get; set; } = null!;
        public DbSet<DataModel> Data { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure your model here if needed.
        }
    }

}
