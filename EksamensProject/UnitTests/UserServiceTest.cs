using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EksamensProject.Core.ApplicationService;
using EksamensProject.Core.ApplicationService.Implementation;
using EksamensProject.Core.DomainService;
using EksamensProject.Core.Entity;
using EksamensProject.Infrastructure.SQL;
using EksamensProject.Infrastructure.SQL.Repositories;
using FluentValidation;
using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace UnitTests
{
    public class UserServiceTest
    {
        private readonly UserValidator _validator = new UserValidator();
        
        [Fact]
        public void CreateNullUserThrowsException()
        {
            var userRepo = new Mock<IUserRepository<User>>();
            IUserService service = new UserService(userRepo.Object);
            
            Exception ex = Assert.Throws<InvalidDataException>(() => 
                service.CreateUser(null));
            Assert.Equal("User cannot be null", ex.Message);

        }
        [Theory]
        [InlineData("Max", "max@example.com")]
        public void CreateNewUserWorking_Test(string name, string email)
        {
            var userRepo = new Mock<IUserRepository<User>>();
            IUserService service = new UserService(userRepo.Object);
            
            var user = service.CreateNewUser(name, email);
            var result = _validator.TestValidate(user);
            
            result.ShouldNotHaveValidationErrorFor(u => u.Name);
            result.ShouldNotHaveValidationErrorFor(u => u.Email);
        }
        
        [Theory]
        [InlineData("Max", "")]
        public void CreateNewUserMissingEmailThrowException(string name, string email)
        {
            var userRepo = new Mock<IUserRepository<User>>();
            IUserService service = new UserService(userRepo.Object);
            
            var user = service.CreateNewUser(name, email);
            var result = _validator.TestValidate(user);
            
            result.ShouldHaveValidationErrorFor(u => u.Email);
        }
        
        [Theory]
        [InlineData("", "max@example.com")]
        public void CreateNewUserMissingNameThrowException(string name, string email)
        {
            var userRepo = new Mock<IUserRepository<User>>();
            IUserService service = new UserService(userRepo.Object);
            
            var user = service.CreateNewUser(name, email);
            var result = _validator.TestValidate(user);
            
            result.ShouldHaveValidationErrorFor(u => u.Name);
        }
        
        [Theory]
        [InlineData("1234", "max@example.com")]
        public void CreateNewUserInvalidNameThrowException(string name, string email)
        {
            var userRepo = new Mock<IUserRepository<User>>();
            IUserService service = new UserService(userRepo.Object);
            
            
            var user = service.CreateNewUser(name, email);
            var result = _validator.TestValidate(user);
            
            result.ShouldHaveValidationErrorFor(u => u.Name);
        }
        
        [Theory]
        [InlineData("Max", "max@example.com")]
        public void CreateNewUserShouldCallUserRepoOnce(string name, string email)
        {
            var userRepo = new Mock<IUserRepository<User>>();
            IUserService service = new UserService(userRepo.Object);
           
            service.CreateNewUser(name, email);
            
            userRepo.Verify(x => x.Create(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public void FindUserByIdUserFound()
        {
            var user = new User { Id = 1 };
            var userRepo = new Mock<IUserRepository<User>>();
            userRepo.Setup(x => x.ReadById(It.IsAny<int>()))
                .Returns(user);

            IUserService userService = new UserService(userRepo.Object);
            var output = userService.FindUserById(1);
            Assert.True(user.Equals(output));
        }
        
        [Fact]
        public void FindUserByIdUserNotFound()
        {
            var userRepo = new Mock<IUserRepository<User>>();
            IUserService userService = new UserService(userRepo.Object);
           
            Exception ex = Assert.Throws<InvalidDataException>(() => 
                userService.FindUserById(1));
            Assert.Equal("User not found", ex.Message);  
        }
        
        [Fact]
        public void DeleteUserById()
        {
            var user = new User()
            {
                Id = 1,
                Name = "Max",
                Email = "max@example.com"
            };

            var userRepo = new Mock<IUserRepository<User>>();
            IUserService userService = new UserService(userRepo.Object);
            userService.CreateUser(user);

            userRepo.Setup(x => x.ReadById(It.IsAny<int>()))
                .Returns(user);
            userRepo.Setup(r => r.Delete(It.IsAny<int>()));
            
            userService.Delete(user.Id);
            
            userRepo.Verify(r => r.Delete(user.Id));

        }
        
    }
}