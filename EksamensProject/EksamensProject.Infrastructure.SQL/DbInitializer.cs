using System;
using EksamensProject.Core.Entity;
using Microsoft.EntityFrameworkCore.Internal;

namespace EksamensProject.Infrastructure.SQL
{
    public class DbInitializer : IDbInitializer
    {
        public void Initialize(EksamensProjectContext context)
        {
            // Deletes and creates database
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();


            var admin = new User()
            {
                Name = "Max",
                Email = "max@uldahl.dk"
            };

            var composition = new Composition()
            {
                Duration = 1.1,
                Name = "Yay",
                Style = null,
                Tempo = null,
                Year = DateTime.Now
            };
            context.Users.Add(admin);
            context.Compositions.Add(composition);
            context.SaveChanges();

        }
    }
}