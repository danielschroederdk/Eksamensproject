using System.Collections.Generic;
using EksamensProject.Core.Entity;

namespace EksamensProject.Core.DomainService
{
    public interface ITestimonialRepository
    {
        Testimonial Create(Testimonial review);
        Testimonial ReadById(int id);
        IEnumerable<Testimonial> ReadAll();
        Testimonial Update(Testimonial reviewUpdate);
        Testimonial Delete(int id);
    }
}