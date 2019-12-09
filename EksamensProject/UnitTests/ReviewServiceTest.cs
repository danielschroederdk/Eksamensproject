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
        public void CreateNewReviewWithHeaderMissingThrowsException2()
        {
            var userRepo = new Mock<IUserRepository>();
            var reviewRepo = new Mock<ITestimonialRepository>();
            ITestimonialService reviewService = new TestimonialService(reviewRepo.Object, userRepo.Object);

            userRepo.Setup(x => x.ReadById(It.IsAny<int>()))
                .Returns(new User(){Id = 1});
            
            var review = reviewService.CreateNewReview(1, "", "dolor sit amet");

            var result = _validator.TestValidate(review);
            result.ShouldHaveValidationErrorFor(u => u.ReviewHeader);
            result.ShouldNotHaveValidationErrorFor(u => u.User);
            result.ShouldNotHaveValidationErrorFor(u => u.ReviewBody);

        }
        [Fact]
        public void CreateNewReviewWithHeaderMissingThrowsException()
        {
            var userRepo = new Mock<IUserRepository>();
            userRepo.Setup(x => x.ReadById(It.IsAny<int>()))
               .Returns(new User(){Id = 1});
            
            var reviewRepo = new Mock<ITestimonialRepository>();
            
            ITestimonialService reviewService = new TestimonialService(reviewRepo.Object, userRepo.Object);
            
            var review = new Testimonial()
            {
                User = new User(){Id = 1},
                ReviewBody = "dolor sit amet"
            };
            reviewService.CreateReview(review);
            
            var result = _validator.TestValidate(review);
            result.ShouldHaveValidationErrorFor(u => u.ReviewHeader);
            result.ShouldNotHaveValidationErrorFor(u => u.User);
            result.ShouldNotHaveValidationErrorFor(u => u.ReviewBody);

        }
        [Fact]
        public void CreateNewReviewWithBodyMissingThrowsException()
        {
            var userRepo = new Mock<IUserRepository>();
            userRepo.Setup(x => x.ReadById(It.IsAny<int>()))
                .Returns(new User(){Id = 1});
            
            var reviewRepo = new Mock<ITestimonialRepository>();
            
            ITestimonialService reviewService = new TestimonialService(reviewRepo.Object, userRepo.Object);
            
            var review = new Testimonial()
            {
                User = new User(){Id = 1},
                ReviewHeader = "lorem ipsum"
            };
            reviewService.CreateReview(review);
            
            var result = _validator.TestValidate(review);
            result.ShouldNotHaveValidationErrorFor(u => u.ReviewHeader);
            result.ShouldNotHaveValidationErrorFor(u => u.User);
            result.ShouldHaveValidationErrorFor(u => u.ReviewBody);
        }
        
    }
}