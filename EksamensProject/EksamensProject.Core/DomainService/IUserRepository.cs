using System.Collections.Generic;
using EksamensProject.Core.Entity;

namespace EksamensProject.Core.DomainService
{
    public interface IUserRepository
    {
        User Create(User user);
        User ReadById(int id);
        IEnumerable<User> ReadAll();
        User Update(User userUpdate);
        User Delete(int id);
        
        
    }
}