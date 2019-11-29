using System.Collections.Generic;
using System.Linq;
using EksamensProject.Core.DomainService;
using EksamensProject.Core.Entity;

namespace EksamensProject.Infrastructure.SQL.Repositories
{
    public class UserRepository : IUserRepository
    {
        readonly EksamensProjectContext _ctx;

        public UserRepository(EksamensProjectContext ctx)
        {
            _ctx = ctx;
        }
        public User Create(User user)
        {
            _ctx.Add(user);
            _ctx.SaveChanges();
            return user;
        }

        public User ReadByID(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<User> ReadAll()
        {
            return _ctx.Users.ToList();        
        }

        public User Update(Composition userUpdate)
        {
            throw new System.NotImplementedException();
        }

        public User Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}