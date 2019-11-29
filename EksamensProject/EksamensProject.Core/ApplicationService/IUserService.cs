using System;
using System.Collections.Generic;
using EksamensProject.Core.Entity;

namespace EksamensProject.Core.ApplicationService
{
    public interface IUserService
    {
        User CreateNewUser(String name, String email);
        User CreateUser(User user);
        User FindUserById(int id);
        User Delete(int id);
        User UpdateUser(User userUpdate);
        List<User> GetUsers();
    }
}