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
                .HasMany(user => user.Testimonials)
                .WithOne(testimonial => testimonial.User);
            
            modelBuilder.Entity<User>()
                .HasMany(user => user.Requests)
                .WithOne(request => request.User);
            
            modelBuilder.Entity<Composition>()
                .HasOne(c => c.Style);
            
            modelBuilder.Entity<Composition>()
                .HasOne(c => c.Tempo);

        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Composition> Compositions { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Style> Styles { get; set; }


    }
}