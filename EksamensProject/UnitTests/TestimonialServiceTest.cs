using System;
using System.IO;
using EksamensProject.Core.ApplicationService;
using EksamensProject.Core.ApplicationService.Implementation;
using EksamensProject.Core.DomainService;
using EksamensProject.Core.Entity;
using FluentValidation.TestHelper;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace UnitTests
{
    public class TestimonialServiceTest
    {
        private readonly ReviewValidator _validator = new ReviewValidator();
        
        [Fact]
        public void CreateNullTestimonialThrowsException()
        {
            var testimonialRepo = new Mock<ITestimonialRepository>();
            var userRepo = new Mock<IUserRepository<User>>();

            ITestimonialService service = new TestimonialService(testimonialRepo.Object, userRepo.Object);

            Exception ex = Assert.Throws<InvalidDataException>(() => 
                service.CreateReview(null));
            Assert.Equal("Testimonial cannot be null", ex.Message);

        }
        [Fact]
        public void CreateNewTestimonialWithHeaderMissingThrowsException2()
        {
            var userRepo = new Mock<IUserRepository<User>>();
            var reviewRepo = new Mock<ITestimonialRepository>();
            ITestimonialService reviewService = new TestimonialService(reviewRepo.Object, userRepo.Object);

            userRepo.Setup(x => x.ReadById(It.IsAny<int>()))
                .Returns(new User(){Id = 1});
            
            var review = reviewService.CreateNewReview(1, "", "dolor sit amet");

            var result = _validator.TestValidate(review);
            result.ShouldHaveValidationErrorFor(u => u.TestimonialHeader);

        }
       
        [Fact]
        public void CreateNewTestimonialWithBodyMissingThrowsException()
        {
            var userRepo = new Mock<IUserRepository<User>>();
            userRepo.Setup(x => x.ReadById(It.IsAny<int>()))
                .Returns(new User(){Id = 1});
            
            var reviewRepo = new Mock<ITestimonialRepository>();
            
            ITestimonialService reviewService = new TestimonialService(reviewRepo.Object, userRepo.Object);
            
            var review = reviewService.CreateNewReview(1, "lorem ipsum", "");

            var result = _validator.TestValidate(review);
            result.ShouldHaveValidationErrorFor(u => u.TestimonialBody);
        }
        [Fact]
        public void CreateNewTestimonialWithNullUserThrowsException()
        {
            var userRepo = new Mock<IUserRepository<User>>();
            var reviewRepo = new Mock<ITestimonialRepository>();
            ITestimonialService reviewService = new TestimonialService(reviewRepo.Object, userRepo.Object);

            var testimonial = new Testimonial()
            {
                User = null,
                TestimonialBody = "lorem ipsum",
                TestimonialHeader = "dolor sit amet"
            };

            reviewService.CreateReview(testimonial);
            
            var result = _validator.TestValidate(testimonial);
            result.ShouldHaveValidationErrorFor(t => t.User);
        }
        

        [Fact]
        public void CreateNewTestimonialShouldCallRepoOnce()
        {
            var userRepo = new Mock<IUserRepository<User>>();
            var reviewRepo = new Mock<ITestimonialRepository>();
            ITestimonialService reviewService = new TestimonialService(reviewRepo.Object, userRepo.Object);
            
            reviewService.CreateNewReview(1, "lorem ipsum", "dolor sit amet");
            
            reviewRepo.Verify(x => x.Create(It.IsAny<Testimonial>()), Times.Once);
        }
        
        [Fact]
        public void FindTestimonialByIdTestimonialFound()
        {
            var userRepo = new Mock<IUserRepository<User>>();
            var reviewRepo = new Mock<ITestimonialRepository>();

            var testimonial = new Testimonial() {Id = 1};
            ITestimonialService reviewService = new TestimonialService(reviewRepo.Object, userRepo.Object);

            reviewRepo.Setup(x => x.ReadById(It.IsAny<int>()))
                .Returns(testimonial);

            var output = reviewService.FindReviewById(1);
            Assert.True(testimonial.Equals(output));
        }
        [Fact]
        public void FindTestimonialByIdTestimonialNotFound()
        {
            var userRepo = new Mock<IUserRepository<User>>();
            var reviewRepo = new Mock<ITestimonialRepository>();

            ITestimonialService reviewService = new TestimonialService(reviewRepo.Object, userRepo.Object);

            Exception ex = Assert.Throws<InvalidDataException>(() =>
                reviewService.FindReviewById(1));
            
            Assert.Equal("Testimonial not found", ex.Message);

        }
        
        
        [Fact]
        public void DeleteTestimonialById()
        {
            var userRepo = new Mock<IUserRepository<User>>();
            var reviewRepo = new Mock<ITestimonialRepository>();
            ITestimonialService reviewService = new TestimonialService(reviewRepo.Object, userRepo.Object);
            
            userRepo.Setup(x => x.ReadById(It.IsAny<int>()))
                .Returns(new User(){Id = 1});
            
            var testimonial = reviewService.CreateNewReview(1, "lorem ipsum", "dolor sit amet");
            
            reviewRepo.Setup(x => x.ReadById(It.IsAny<int>()))
                .Returns(testimonial);
            reviewRepo.Setup(r => r.Delete(It.IsAny<int>()));
            
            reviewService.Delete(testimonial.Id);
            
            reviewRepo.Verify(r => r.Delete(testimonial.Id));

        }
    }
}