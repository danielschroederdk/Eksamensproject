using System.Collections.Generic;
using System.Linq;
using EksamensProject.Core.DomainService;
using EksamensProject.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace EksamensProject.Infrastructure.SQL.Repositories
{
    public class RequestRepository : IRequestRepository
    {
        readonly EksamensProjectContext _ctx;

        public RequestRepository(EksamensProjectContext ctx)
        {
            _ctx = ctx;
        }

        public Request Create(Request request)
        {
            _ctx.Add(request);
            _ctx.SaveChanges();
            return request;
        }

        public Request ReadById(int id)
        {
            return _ctx.Requests.Include(i => i.User).FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Request> ReadAll()
        {
            return _ctx.Requests.ToList();        

        }

        public Request Update(Request requestUpdate)
        {
            _ctx.Entry(requestUpdate).State = EntityState.Modified;
            _ctx.SaveChanges();
            return requestUpdate;
        }

        public Request Delete(int id)
        {
            var requestToDelete = ReadById(id);
            _ctx.Requests.Remove(requestToDelete);
            _ctx.SaveChanges();
            return requestToDelete;
        }
    }
}