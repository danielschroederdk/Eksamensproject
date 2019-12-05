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
        private readonly ReviewValidator _validator = new ReviewValidator();

        [Fact]
        public void CreateNewReviewWithHeaderMissingThrowsException()
        {
            var userRepo = new Mock<IUserRepository>();
            userRepo.Setup(x => x.ReadById(It.IsAny<int>()))
               .Returns(new User(){Id = 1});
            
            var reviewRepo = new Mock<IReviewRepository>();
            
            IReviewService reviewService = new ReviewService(reviewRepo.Object, userRepo.Object);
            
            var review = new Review()
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
        public void C2reateNewReviewWithBodyMissingThrowsException()
        {
            var userRepo = new Mock<IUserRepository>();
            userRepo.Setup(x => x.ReadById(It.IsAny<int>()))
                .Returns(new User(){Id = 1});
            
            var reviewRepo = new Mock<IReviewRepository>();
            
            IReviewService reviewService = new ReviewService(reviewRepo.Object, userRepo.Object);
            
            var review = new Review()
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