using System.Collections.Generic;
using System.Linq;
using EksamensProject.Core.DomainService;
using EksamensProject.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace EksamensProject.Infrastructure.SQL.Repositories
{
    public class TestimonialRepository : ITestimonialRepository
    {
        readonly EksamensProjectContext _ctx;

        public TestimonialRepository(EksamensProjectContext ctx)
        {
            _ctx = ctx;
        }


        public Testimonial Create(Testimonial review)
        {
            _ctx.Add(review);
            _ctx.SaveChanges();
            return review;
        }

        public Testimonial ReadById(int id)
        {
            return _ctx.Reviews.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Testimonial> ReadAll()
        {
            return _ctx.Reviews.ToList();        
        }

        public Testimonial Update(Testimonial reviewUpdate)
        {
            _ctx.Entry(reviewUpdate).State = EntityState.Modified;
            _ctx.SaveChanges();
            return reviewUpdate;
        }

        public Testimonial Delete(int id)
        {
            var reviewToDelete = ReadById(id);
            _ctx.Reviews.Remove(reviewToDelete);
            _ctx.SaveChanges();
            return reviewToDelete;
        }
    }
}