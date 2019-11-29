using System.Collections.Generic;
using System.Linq;
using EksamensProject.Core.DomainService;
using EksamensProject.Core.Entity;
using Microsoft.EntityFrameworkCore;

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
            return _ctx.Users.FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<User> ReadAll()
        {
            return _ctx.Users.ToList();        
        }

        public User Update(User userUpdate)
        {
            _ctx.Entry(userUpdate).State = EntityState.Modified;
            _ctx.SaveChanges();
            return userUpdate;
        }

        public User Delete(int id)
        {
            var userToDelete = ReadByID(id);
            _ctx.Users.Remove(userToDelete);
            _ctx.SaveChanges();
            return userToDelete;
        }
    }
}