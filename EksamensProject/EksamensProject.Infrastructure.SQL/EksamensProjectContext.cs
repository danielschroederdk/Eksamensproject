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
            
            modelBuilder.Entity<User>()
                .HasMany(user => user.Reviews)
                .WithOne(review => review.User);
            
            
            modelBuilder.Entity<User>()
                .HasMany(user => user.Requests)
                .WithOne(request => request.User);
            
            //add style relation
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Composition> Compositions { get; set; }
        public DbSet<Testimonial> Reviews { get; set; }
        public DbSet<Request> Requests { get; set; }


    }
}