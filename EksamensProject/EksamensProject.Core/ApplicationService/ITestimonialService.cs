using System;
using System.Collections.Generic;
using EksamensProject.Core.Entity;

namespace EksamensProject.Core.ApplicationService
{
    public interface ITestimonialService
    {
        Testimonial CreateNewReview(int userId, String header, String body);
        Testimonial CreateReview(Testimonial review);
        Testimonial FindReviewById(int id);
        Testimonial Delete(int id);
        Testimonial UpdateReview(Testimonial reviewUpdate);
        List<Testimonial> GetReviews();
    }
}