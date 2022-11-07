using final_project.Models;
using Microsoft.EntityFrameworkCore;


namespace final_project.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<UserModel> Users { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {

        }
    }
}
