using EksamensProject.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace EksamensProject.Infrastructure.SQL
{
    public class EksamensProjectContext : DbContext
    {
        public EksamensProjectContext(DbContextOptions<EksamensProjectContext> opt) : base(opt)
        {
            
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
        
        public DbSet<User> Users { get; set; }
        
    }
}