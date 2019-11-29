using System;
using System.Collections.Generic;
using System.Linq;
using EksamensProject.Core.DomainService;
using EksamensProject.Core.Entity;

namespace EksamensProject.Core.ApplicationService.Implementation
{
    public class UserService : IUserService
    {
        readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public User CreateNewUser(string name, string email)
        {
            var newUser = new User()
            {
                Name = name,
                Email = email
            };
            _userRepository.Create(newUser);
            return newUser;
        }

        public User CreateUser(User user)
        {
            return _userRepository.Create(user);
        }

        public User FindUserById(int id)
        {
            throw new NotImplementedException();
        }

        public User Delete(int id)
        {
            throw new NotImplementedException();
        }

        public User UpdateUser(User userUpdate)
        {
            throw new NotImplementedException();
        }

        public List<User> GetUsers()
        {
            return _userRepository.ReadAll().ToList();
        }
    }
}