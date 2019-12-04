using System;
using System.Collections.Generic;
using EksamensProject.Core.Entity;

namespace EksamensProject.Core.ApplicationService
{
    public interface IReviewService
    {
        Review CreateNewReview(int userId, String header, String body);
        Review CreateReview(Review review);
        Review FindReviewById(int id);
        Review Delete(int id);
        Review UpdateReview(Review reviewUpdate);
        List<Review> GetReviews();
    }
}