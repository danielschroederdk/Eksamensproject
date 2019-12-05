using System;
using System.IO;
using EksamensProject.Core.ApplicationService;
using EksamensProject.Core.ApplicationService.Implementation;
using EksamensProject.Core.DomainService;
using EksamensProject.Core.Entity;
using EksamensProject.Infrastructure.SQL;
using EksamensProject.Infrastructure.SQL.Repositories;
using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace UnitTests
{
    public class UserServiceTest
    {
        private readonly UserValidator _validator = new UserValidator();

        [Theory]
        [InlineData("Max", "max@example.com")]
        public void CreateNewUserWorking_Test(string name, string email)
        {
            var userRepo = new Mock<IUserRepository>();
            IUserService service = new UserService(userRepo.Object);
            
            var user = service.CreateNewUser(name, email);
            var result = _validator.TestValidate(user);
            
            result.ShouldNotHaveValidationErrorFor(u => u.Name);
            result.ShouldNotHaveValidationErrorFor(u => u.Email);

            Assert.Equal(user.Name, name);
            Assert.Equal(user.Email,email);
        }
        
        [Theory]
        [InlineData("Max", "")]
        public void CreateNewUserMissingEmailThrowException(string name, string email)
        {
            var userRepo = new Mock<IUserRepository>();
            IUserService service = new UserService(userRepo.Object);
            
            var user = service.CreateNewUser(name, email);
            var result = _validator.TestValidate(user);
            
            result.ShouldHaveValidationErrorFor(u => u.Email);
        }
        
        [Theory]
        [InlineData("", "max@example.com")]
        public void CreateNewUserMissingNameThrowException(string name, string email)
        {
            var userRepo = new Mock<IUserRepository>();
            IUserService service = new UserService(userRepo.Object);
            
            var user = service.CreateNewUser(name, email);
            var result = _validator.TestValidate(user);
            
            result.ShouldHaveValidationErrorFor(u => u.Name);
        }
        
        [Theory]
        [InlineData("1234", "max@example.com")]
        public void CreateNewUserInvalidNameThrowException(string name, string email)
        {
            var userRepo = new Mock<IUserRepository>();
            IUserService service = new UserService(userRepo.Object);
            
            var user = service.CreateNewUser(name, email);
            var result = _validator.TestValidate(user);
            
            result.ShouldHaveValidationErrorFor(u => u.Name);
        }
        
        [Theory]
        [InlineData("Max", "max@example.com")]
        public void CreateNewUserShouldCallUserRepoOnce(string name, string email)
        {
            var userRepo = new Mock<IUserRepository>();
            IUserService service = new UserService(userRepo.Object);
           
            service.CreateNewUser(name, email);
            
            userRepo.Verify(x => x.Create(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public void FindUserByIdUserFound()
        {
            var userRepo = new Mock<IUserRepository>();
            IUserService service = new UserService(userRepo.Object);

            
            var user = new User()
            {
                Id = 1, 
                Name = "Max",
                Email = "max@example.com"
            };

            var userCreated = service.CreateUser(user);
            var userFound = service.FindUserById(1);
            
            Assert.Same(userCreated, userFound);
        }
        
        public void ReadUserByIdUserNotFoundThrowError(int Id)
        {
            var userRepo = new Mock<IUserRepository>();
 
            userRepo.Setup(x => x.ReadById(It.IsAny<int>()))
                .Returns(new User(){Id = 1});
        }
    }
}