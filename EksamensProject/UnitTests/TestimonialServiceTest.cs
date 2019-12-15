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
    public class ReviewServiceTest
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
        public void CreateNewTestimonialWithHeaderMissingThrowsException()
        {
            var userRepo = new Mock<IUserRepository<User>>();
            userRepo.Setup(x => x.ReadById(It.IsAny<int>()))
               .Returns(new User(){Id = 1});
            
            var reviewRepo = new Mock<ITestimonialRepository>();
            
            ITestimonialService reviewService = new TestimonialService(reviewRepo.Object, userRepo.Object);
            
            var review = new Testimonial()
            {
                User = new User(){Id = 1},
                TestimonialBody = "dolor sit amet"
            };
            reviewService.CreateReview(review);
            
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
            
            var review = new Testimonial()
            {
                User = new User(){Id = 1},
                TestimonialHeader = "lorem ipsum"
            };
            reviewService.CreateReview(review);
            
            var result = _validator.TestValidate(review);
            result.ShouldHaveValidationErrorFor(u => u.TestimonialBody);
        }
        
    }
}