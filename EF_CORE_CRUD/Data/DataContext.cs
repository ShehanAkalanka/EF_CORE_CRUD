using Microsoft.EntityFrameworkCore;

namespace EF_CORE_CRUD.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
    }
}
