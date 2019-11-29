using System;
using System.Collections.Generic;
using System.IO;
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
            return _userRepository.ReadByID(id) == null
                ? throw new InvalidDataException("User not found")
                : _userRepository.ReadByID(id);
        }

        public User Delete(int id)
        {
            return FindUserById(id) == null
                ? throw new InvalidDataException("User not found or already deleted")
                : _userRepository.Delete(id);
        }

        public User UpdateUser(User userUpdate)
        {
            return _userRepository.Update(userUpdate);
        }

        public List<User> GetUsers()
        {
            return _userRepository.ReadAll().ToList();
        }
    }
}