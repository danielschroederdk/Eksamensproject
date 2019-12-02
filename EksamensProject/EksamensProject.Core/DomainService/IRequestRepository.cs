using System.Collections.Generic;
using EksamensProject.Core.Entity;

namespace EksamensProject.Core.DomainService
{
    public interface IRequestRepository
    {
        Request Create(Request request);
        Request ReadById(int id);
        IEnumerable<Request> ReadAll();
        Request Update(Request requestUpdate);
        Request Delete(int id);
    }
}