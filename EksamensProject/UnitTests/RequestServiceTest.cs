using System;
using System.IO;
using EksamensProject.Core.ApplicationService;
using EksamensProject.Core.ApplicationService.Implementation;
using EksamensProject.Core.DomainService;
using EksamensProject.Core.Entity;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

namespace UnitTests
{
    public class RequestServiceTest
    {
        
        private readonly RequestValidator _validator = new RequestValidator();

        
        [Fact]
        public void CreateNullRequestThrowsException()
        {
            var userRepo = new Mock<IUserRepository<User>>();

            var requestRepo = new Mock<IRequestRepository>();
            var service = new RequestService(requestRepo.Object, userRepo.Object);
            
            Exception ex = Assert.Throws<InvalidDataException>(() => 
                service.CreateRequest(null));
            Assert.Equal("Request cannot be null", ex.Message);
        }

        [Fact]
        public void CreateNewRequestWithHeaderMissingThrowsException()
        {
            var userRepo = new Mock<IUserRepository<User>>();
            var requestRepo = new Mock<IRequestRepository>();
            var requestService = new RequestService(requestRepo.Object, userRepo.Object);

            userRepo.Setup(x => x.ReadById(It.IsAny<int>()))
                .Returns(new User(){Id = 1});
            
            var request = requestService.CreateNewRequest(1, "", "dolor sit amet");

            var result = _validator.TestValidate(request);
            result.ShouldHaveValidationErrorFor(r => r.RequestHeader);
        }
        
        [Fact]
        public void CreateNewRequestWithBodyMissingThrowsException()
        {
            var userRepo = new Mock<IUserRepository<User>>();
            var requestRepo = new Mock<IRequestRepository>();
            var requestService = new RequestService(requestRepo.Object, userRepo.Object);
            
            userRepo.Setup(x => x.ReadById(It.IsAny<int>()))
                .Returns(new User(){Id = 1});
            
            var request = requestService.CreateNewRequest(1, "lorem ipsum", "");

            var result = _validator.TestValidate(request);
            result.ShouldHaveValidationErrorFor(u => u.RequestBody);
        }
        
        [Fact]
        public void CreateNewRequestWithNullUserThrowsException()
        {
            var userRepo = new Mock<IUserRepository<User>>();
            var requestRepo = new Mock<IRequestRepository>();
            var requestService = new RequestService(requestRepo.Object, userRepo.Object);

            var request = new Request()
            {
                User = null,
                RequestBody = "lorem ipsum",
                RequestHeader = "dolor sit amet"
            };

            requestService.CreateRequest(request);
            
            var result = _validator.TestValidate(request);
            result.ShouldHaveValidationErrorFor(r => r.User);
        }
        
        [Fact]
        public void CreateNewRequestShouldCallRepoOnce()
        {
            var userRepo = new Mock<IUserRepository<User>>();
            var requestRepo = new Mock<IRequestRepository>();
            var requestService = new RequestService(requestRepo.Object, userRepo.Object);
            
            requestService.CreateNewRequest(1, "lorem ipsum", "dolor sit amet");
            
            requestRepo.Verify(x => x.Create(It.IsAny<Request>()), Times.Once);
        }
        
        [Fact]
        public void FindRequestByIdRequestFound()
        {
            var userRepo = new Mock<IUserRepository<User>>();
            var requestRepo = new Mock<IRequestRepository>();
            var requestService = new RequestService(requestRepo.Object, userRepo.Object);

            var request = new Request() {Id = 1};

            requestRepo.Setup(x => x.ReadById(It.IsAny<int>()))
                .Returns(request);

            var output = requestService.FindRequestById(1);
            Assert.True(request.Equals(output));
        }
        
        [Fact]
        public void FindRequestByIdRequestNotFound()
        {
            var userRepo = new Mock<IUserRepository<User>>();
            var requestRepo = new Mock<IRequestRepository>();
            var requestService = new RequestService(requestRepo.Object, userRepo.Object);
            
            Exception ex = Assert.Throws<InvalidDataException>(() =>
                requestService.FindRequestById(1));
            
            Assert.Equal("Request not found", ex.Message);
        }
        
        [Fact]
        public void DeleteRequestById()
        {
            var userRepo = new Mock<IUserRepository<User>>();
            var requestRepo = new Mock<IRequestRepository>();
            var requestService = new RequestService(requestRepo.Object, userRepo.Object);

            userRepo.Setup(x => x.ReadById(It.IsAny<int>()))
                .Returns(new User(){Id = 1});
            
            var request = requestService.CreateNewRequest(1, "lorem ipsum", "dolor sit amet");
            
            requestRepo.Setup(x => x.ReadById(It.IsAny<int>()))
                .Returns(request);
            requestRepo.Setup(r => r.Delete(It.IsAny<int>()));
            
            requestService.Delete(request.Id);
            
            requestRepo.Verify(r => r.Delete(request.Id));

        }
        
    }
}