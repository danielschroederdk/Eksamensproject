using System;
using System.Collections.Generic;
using EksamensProject.Core.Entity;

namespace EksamensProject.Core.ApplicationService
{
    public interface IRequestService
    {
        Request CreateNewRequest(User user, String header, String body);
        Request CreateRequest(Request request);
        Request FindRequestById(int id);
        Request Delete(int id);
        Request UpdateRequest(Request requestUpdate);
        List<Request> GetRequests();
    }
}