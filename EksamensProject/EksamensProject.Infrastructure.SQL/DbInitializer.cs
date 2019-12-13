using System;
using EksamensProject.Core.ApplicationService;
using EksamensProject.Core.Entity;
using Microsoft.EntityFrameworkCore.Internal;

namespace EksamensProject.Infrastructure.SQL
{
    public class DbInitializer : IDbInitializer
    {
        private IAuthenticationService _authenticationService;

        public DbInitializer(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        public void Initialize(EksamensProjectContext context)
        {
            // Deletes and creates database
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            
            // Being salty
            string password = "1234";
            byte[] passwordHashMax, passwordSaltMax, passwordHashThomas, passwordSaltThomas;
            _authenticationService.CreatePasswordHash(password, out passwordHashMax, out passwordSaltMax);
            _authenticationService.CreatePasswordHash(password, out passwordHashThomas, out passwordSaltThomas);

            var admin = new User()
            {
                Name = "Max",
                Email = "max@uldahl.dk",
                PasswordHash = passwordHashMax,
                PasswordSalt = passwordSaltMax,
                Role = Role.Admin
            };
            
            var user = new User()
            {
                Name = "Thomas",
                Email = "thomas@example.dk",
                PasswordHash = passwordHashThomas,
                PasswordSalt = passwordSaltThomas,
                Role = Role.User
            };

            var style = new Style()
            {
                Era = "New time",
                Name = "Wauw"
            };
            
            var tempo = new Tempo()
            {
                TempoMarking = TempoMarking.Adagietto,
                TimeSignature = TimeSignature.FourFour,
                BeatsPerMinute = 40
            };

            var composition = new Composition()
            {
                Duration = 2.12,
                Name = "Heaven & Hell",
                Style = style,
                Tempo = tempo,
                Year = "2012",
                URL = "https://www.mboxdrive.com/Heaven%20&%20Hell.mp3",
                PictureURL = "https://i.ibb.co/JdftBvn/fuldmaane.jpg",
                Comment = "\"Epic and adventurous composition\""
            };

            var composition2 = new Composition()
            {
                Duration = 0.58,
                Name = "Watch Out",
                Style = style,
                Tempo = tempo,
                Year = "2012",
                URL = "https://www.mboxdrive.com/Watch%20Out%20(suspense).mp3",
                PictureURL = "https://i.ibb.co/JdftBvn/fuldmaane.jpg",
                Comment = "\"Incredible thrill and tension\""
            };
            
            var composition3 = new Composition()
            {
                Duration = 1.04,
                Name = "Someone Is Watching",
                Style = style,
                Tempo = tempo,
                Year = "2012",
                URL = "https://www.mboxdrive.com/Someone%20Is%20Watching.mp3",
                PictureURL = "https://i.ibb.co/JdftBvn/fuldmaane.jpg",
                Comment = "\"Suspenseful orchestration\""
            };
            

            
            var testimonial = new Testimonial()
            {
                User = admin,
                TestimonialHeader = "Excellent",
                TestimonialBody = "NICE"
            };
            
            var testimonial2 = new Testimonial()
            {
                User = user,
                TestimonialHeader = "Excellent",
                TestimonialBody = "NICE"
            };
            
            var request = new Request()
            {
                User = user,
                RequestHeader = "Classical music for movie",
                RequestBody = "New danish movie about..."
                
            };
            
            // Adding
            context.Users.Add(admin);
            context.Compositions.Add(composition);
            context.Compositions.Add(composition2);
            context.Compositions.Add(composition3);
            context.Testimonials.Add(testimonial);
            context.Testimonials.Add(testimonial2);
            context.Requests.Add(request);
            context.SaveChanges();

        }
    }
}