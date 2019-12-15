using System.Collections.Generic;
using System.IO;
using System.Linq;
using EksamensProject.Core.DomainService;
using EksamensProject.Core.Entity;

namespace EksamensProject.Core.ApplicationService.Implementation
{
    public class TestimonialService : ITestimonialService
    {
        readonly ITestimonialRepository _reviewRepository;
        readonly IUserRepository<User> _userRepository;

        public TestimonialService(ITestimonialRepository reviewRepository, IUserRepository<User> userRepository)
        {
            _reviewRepository = reviewRepository;
            _userRepository = userRepository;
        }

        public Testimonial CreateNewReview(int userId, string header, string body)
        {
            var user = _userRepository.ReadById(userId);
            var newReview = new Testimonial()
            {
                User = user,
                TestimonialHeader = header,
                TestimonialBody = body
            };
            
            _reviewRepository.Create(newReview);
            return newReview;
        }
        
        public Testimonial CreateReview(Testimonial review)
        {
            if (review == null)
            {
                throw new InvalidDataException("Testimonial cannot be null");
            }
            return _reviewRepository.Create(review);
        }

        public Testimonial FindReviewById(int id)
        {
            return _reviewRepository.ReadById(id) == null
                ? throw new InvalidDataException("Review not found")
                : _reviewRepository.ReadById(id);           }

        public Testimonial Delete(int id)
        {
            return FindReviewById(id) == null
                ? throw new InvalidDataException("Review not found or already deleted")
                : _reviewRepository.Delete(id);
        }

        public Testimonial UpdateReview(Testimonial reviewUpdate)
        {
            return _reviewRepository.Update(reviewUpdate);
        }

        public List<Testimonial> GetReviews()
        {
            return _reviewRepository.ReadAll().ToList();
        }
    }
}