using System.Collections.Generic;
using System.IO;
using System.Linq;
using EksamensProject.Core.DomainService;
using EksamensProject.Core.Entity;

namespace EksamensProject.Core.ApplicationService.Implementation
{
    public class ReviewService : IReviewService
    {
        readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public Review CreateNewReview(User user, string header, string body)
        {
            var newReview = new Review()
            {
                User = user,
                ReviewHeader = header,
                ReviewBody = body
            };
            
            _reviewRepository.Create(newReview);
            return newReview;
        }
        
        public Review CreateReview(Review review)
        {
            return _reviewRepository.Create(review);
        }

        public Review FindReviewById(int id)
        {
            return _reviewRepository.ReadById(id) == null
                ? throw new InvalidDataException("Review not found")
                : _reviewRepository.ReadById(id);           }

        public Review Delete(int id)
        {
            return FindReviewById(id) == null
                ? throw new InvalidDataException("Review not found or already deleted")
                : _reviewRepository.Delete(id);
        }

        public Review UpdateReview(Review reviewUpdate)
        {
            return _reviewRepository.Update(reviewUpdate);
        }

        public List<Review> GetReviews()
        {
            return _reviewRepository.ReadAll().ToList();
        }
    }
}