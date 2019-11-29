using System.Collections.Generic;
using EksamensProject.Core.Entity;

namespace EksamensProject.Core.DomainService
{
    public interface IUserRepository
    {
        User Create(User user);
        User ReadByID(int id);
        IEnumerable<User> ReadAll();
        User Update(Composition userUpdate);
        User Delete(int id);
        
        
    }
}