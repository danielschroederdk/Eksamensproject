using System;
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
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly ReviewValidator _validator = new ReviewValidator();

        public ReviewServiceTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }
        [Fact]
        public void CreateNewTestimonialWithHeaderMissingThrowsException2()
        {
            var userRepo = new Mock<IUserRepository>();
            var reviewRepo = new Mock<ITestimonialRepository>();
            ITestimonialService reviewService = new TestimonialService(reviewRepo.Object, userRepo.Object);

            userRepo.Setup(x => x.ReadById(It.IsAny<int>()))
                .Returns(new User(){Id = 1});
            
            var review = reviewService.CreateNewReview(1, "", "dolor sit amet");

            var result = _validator.TestValidate(review);
            result.ShouldHaveValidationErrorFor(u => u.TestimonialHeader);
            result.ShouldNotHaveValidationErrorFor(u => u.User);
            result.ShouldNotHaveValidationErrorFor(u => u.TestimonialBody);

        }
        [Fact]
        public void CreateNewTestimonialWithHeaderMissingThrowsException()
        {
            var userRepo = new Mock<IUserRepository>();
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
            result.ShouldNotHaveValidationErrorFor(u => u.User);
            result.ShouldNotHaveValidationErrorFor(u => u.TestimonialBody);

        }
        [Fact]
        public void CreateNewTestimonialWithBodyMissingThrowsException()
        {
            var userRepo = new Mock<IUserRepository>();
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
            result.ShouldNotHaveValidationErrorFor(u => u.TestimonialHeader);
            result.ShouldNotHaveValidationErrorFor(u => u.User);
            result.ShouldHaveValidationErrorFor(u => u.TestimonialBody);
        }
        
    }
}