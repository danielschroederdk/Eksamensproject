using System.Collections.Generic;
using System.Linq;
using EksamensProject.Core.DomainService;
using EksamensProject.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace EksamensProject.Infrastructure.SQL.Repositories
{
    public class UserRepository : IUserRepository<User>
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

        public User ReadById(int id)
        {
            return _ctx.Users.Include(i => i.Testimonials).Include(i => i.Requests).FirstOrDefault(u => u.Id == id);
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
            var userToDelete = ReadById(id);
            _ctx.Users.Remove(userToDelete);
            _ctx.SaveChanges();
            return userToDelete;
        }
    }
}