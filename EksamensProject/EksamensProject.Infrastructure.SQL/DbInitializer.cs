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
            
            var user2 = new User()
            {
                Name = "Sebastian",
                Email = "thomas@example.dk",
                PasswordHash = passwordHashThomas,
                PasswordSalt = passwordSaltThomas,
                Role = Role.User
            };
            
            var user3 = new User()
            {
                Name = "Wolffgang",
                Email = "thomas@example.dk",
                PasswordHash = passwordHashThomas,
                PasswordSalt = passwordSaltThomas,
                Role = Role.User
            };
            
            var fifties = new Style()
            {
                Era = "1950's Film Score",
                Name = "Classical Modern Film Score"
            };
            
            var golden = new Style()
            {
                Era = "1913-1929",
                Name = "Golden Age, Hollywood"
            };
            
            var postmodern = new Style()
            {
                Era = "1970-",
                Name = "Postmodern, Contemporary"
            };
            
            
            var adagietto = new Tempo()
            {
                TempoMarking = TempoMarking.Adagietto,
                TimeSignature = TimeSignature.FourFour,
                BeatsPerMinute = 70
            };
            
            var allegreto = new Tempo()
            {
                TempoMarking = TempoMarking.Allegreto,
                TimeSignature = TimeSignature.ThreeFour,
                BeatsPerMinute = 120
            };

            var allegreto2 = new Tempo()
            {
                TempoMarking = TempoMarking.Allegreto,
                TimeSignature = TimeSignature.FourFour,
                BeatsPerMinute = 120
            };
            
            var moderato = new Tempo()
            {
                TempoMarking = TempoMarking.Moderato,
                TimeSignature = TimeSignature.FourFour,
                BeatsPerMinute = 110
            };
            
            
            var larghetto = new Tempo()
            {
                TempoMarking = TempoMarking.Larghetto,
                TimeSignature = TimeSignature.FourFour,
                BeatsPerMinute = 60
            };
            
            var andante = new Tempo()
            {
                TempoMarking = TempoMarking.Andante,
                TimeSignature = TimeSignature.FourFour,
                BeatsPerMinute = 95
            };
            
            var composition = new Composition()
            {
                Duration = 2.12,
                Name = "Heaven & Hell",
                Style = postmodern,
                Tempo = andante,
                Year = "2012",
                URL = "https://www.mboxdrive.com/Heaven%20&%20Hell.mp3",
                PictureURL = "https://i.ibb.co/JdftBvn/fuldmaane.jpg",
                Comment = "\"Epic and adventurous composition\""
            };

            var composition2 = new Composition()
            {
                Duration = 0.58,
                Name = "Watch Out",
                Style = fifties,
                Tempo = allegreto,
                Year = "2012",
                URL = "https://www.mboxdrive.com/Watch%20Out%20(suspense).mp3",
                PictureURL = "https://i.ibb.co/JdftBvn/fuldmaane.jpg",
                Comment = "\"Incredible thrill and tension\""
            };
            
            var composition3 = new Composition()
            {
                Duration = 1.04,
                Name = "Someone Is Watching",
                Style = postmodern,
                Tempo = adagietto,
                Year = "2012",
                URL = "https://www.mboxdrive.com/Someone%20Is%20Watching.mp3",
                PictureURL = "https://i.ibb.co/JdftBvn/fuldmaane.jpg",
                Comment = "\"Suspenseful orchestration\""
            };
            var composition4 = new Composition()
            {
                Duration = 7.12,
                Name = "Melancholy",
                Style = postmodern,
                Tempo = larghetto,
                Year = "2014",
                URL = "https://www.mboxdrive.com/Melancholy%20-%2024%20hour%20composition.mp3",
                PictureURL = "https://i.ibb.co/JdftBvn/fuldmaane.jpg",
                Comment = "\"Intense and captivating\""
            };
            var composition5 = new Composition()
            {
                Duration = 0.36,
                Name = "Luna's Theme",
                Style = fifties,
                Tempo = adagietto,
                Year = "2014",
                URL = "https://www.mboxdrive.com/Luna's%20Theme.mp3",
                PictureURL = "https://i.ibb.co/JdftBvn/fuldmaane.jpg",
                Comment = "\"Subtle and gentle soundscape\""
            };
            var composition6 = new Composition()
            {
                Duration = 1.02,
                Name = "Larghetto",
                Style = golden,
                Tempo = larghetto,
                Year = "2014",
                URL = "https://www.mboxdrive.com/Larghetto.mp3",
                PictureURL = "https://i.ibb.co/JdftBvn/fuldmaane.jpg",
                Comment = "\"Hypnotizing harmonics\""
            };
            var composition7 = new Composition()
            {
                Duration = 1.56,
                Name = "Breaking The Surface",
                Style = postmodern,
                Tempo = allegreto,
                Year = "2016",
                URL = "https://www.mboxdrive.com/Breaking%20The%20Surface%20(+end).mp3",
                PictureURL = "https://i.ibb.co/JdftBvn/fuldmaane.jpg",
                Comment = "\"A slow paced thrill\""
            };
            var composition8 = new Composition()
            {
                Duration = 1.01,
                Name = "Beyond Belief",
                Style = fifties,
                Tempo = moderato,
                Year = "2016",
                URL = "https://www.mboxdrive.com/Beyond%20Belief.mp3",
                PictureURL = "https://i.ibb.co/JdftBvn/fuldmaane.jpg",
                Comment = "\"Utmost excitement\""
            };
            var composition9 = new Composition()
            {
                Duration = 2.02,
                Name = "Apocrypha",
                Style = fifties,
                Tempo = andante,
                Year = "2018",
                URL = "https://www.mboxdrive.com/Apocrypha.mp3",
                PictureURL = "https://i.ibb.co/JdftBvn/fuldmaane.jpg",
                Comment = "\"Epic film score\""
            };

            
            var testimonial = new Testimonial()
            {
                User = user,
                TestimonialHeader = "Simply beautiful!",
                TestimonialBody = "We are very pleased with the audio landscaped created by Max.\n" +
                                  "An absolute elevation of a scenematic experience"
            };
            
            var testimonial2 = new Testimonial()
            {
                User = user2,
                TestimonialHeader = "Astonishing and creative",
                TestimonialBody = "The attention to detail and realism is impressive to say the least"
            };
            
            var testimonial3 = new Testimonial()
            {
                User = user3,
                TestimonialHeader = "A pleasure to work with",
                TestimonialBody = "Max has been extremely useful in the making of our project"
            };
            
            var request = new Request()
            {
                User = user,
                RequestHeader = "Classical music for movie",
                RequestBody = "New danish movie about..."
                
            };
            
            // Adding
            context.Users.Add(admin);
            context.Users.Add(user);
            
            context.Styles.Add(golden);
            context.Styles.Add(fifties);
            context.Styles.Add(postmodern);

            context.Compositions.Add(composition);
            context.Compositions.Add(composition2);
            context.Compositions.Add(composition3);
            context.Compositions.Add(composition4);
            context.Compositions.Add(composition5);
            context.Compositions.Add(composition6);
            context.Compositions.Add(composition7);
            context.Compositions.Add(composition8);
            context.Compositions.Add(composition9);

            
            context.Testimonials.Add(testimonial);
            context.Testimonials.Add(testimonial2);
            context.Testimonials.Add(testimonial3);
            context.Requests.Add(request);
            context.SaveChanges();

        }
    }
}