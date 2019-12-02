using System.Collections.Generic;
using System.Linq;
using EksamensProject.Core.DomainService;
using EksamensProject.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace EksamensProject.Infrastructure.SQL.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        readonly EksamensProjectContext _ctx;

        public ReviewRepository(EksamensProjectContext ctx)
        {
            _ctx = ctx;
        }


        public Review Create(Review review)
        {
            _ctx.Add(review);
            _ctx.SaveChanges();
            return review;
        }

        public Review ReadById(int id)
        {
            return _ctx.Reviews.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Review> ReadAll()
        {
            return _ctx.Reviews.ToList();        
        }

        public Review Update(Review reviewUpdate)
        {
            _ctx.Entry(reviewUpdate).State = EntityState.Modified;
            _ctx.SaveChanges();
            return reviewUpdate;
        }

        public Review Delete(int id)
        {
            var reviewToDelete = ReadById(id);
            _ctx.Reviews.Remove(reviewToDelete);
            _ctx.SaveChanges();
            return reviewToDelete;
        }
    }
}