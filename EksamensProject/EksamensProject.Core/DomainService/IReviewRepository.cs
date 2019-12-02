using System.Collections.Generic;
using EksamensProject.Core.Entity;

namespace EksamensProject.Core.DomainService
{
    public interface IReviewRepository
    {
        Review Create(Review review);
        Review ReadById(int id);
        IEnumerable<Review> ReadAll();
        Review Update(Review reviewUpdate);
        Review Delete(int id);
    }
}