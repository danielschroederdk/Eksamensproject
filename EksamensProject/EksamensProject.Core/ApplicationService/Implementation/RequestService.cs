using System.Collections.Generic;
using System.IO;
using System.Linq;
using EksamensProject.Core.DomainService;
using EksamensProject.Core.Entity;

namespace EksamensProject.Core.ApplicationService.Implementation
{
    public class RequestService : IRequestService
    {
        private readonly IRequestRepository _requestRepository;
        readonly IUserRepository<User> _userRepository;


        public RequestService(IRequestRepository requestRepository, IUserRepository<User> userRepository)
        {
            _requestRepository = requestRepository;
            _userRepository = userRepository;
        }


        public Request CreateNewRequest(int userId, string header, string body)
        {
            var user = _userRepository.ReadById(userId);
            
            var newRequest = new Request()
            {
                User = user,
                RequestHeader = header,
                RequestBody = body
            };
            
            _requestRepository.Create(newRequest);
            return newRequest;
            
        }

        public Request CreateRequest(Request request)
        {
            if (request == null)
            {
                throw new InvalidDataException("Request cannot be null");
            }
            return _requestRepository.Create(request);
        }

        public Request FindRequestById(int id)
        {
            return _requestRepository.ReadById(id) == null
                ? throw new InvalidDataException("Request not found")
                : _requestRepository.ReadById(id);
            
        }

        public Request Delete(int id)
        {
            return FindRequestById(id) == null
                ? throw new InvalidDataException("Request not found or already deleted")
                : _requestRepository.Delete(id);
        }

        public Request UpdateRequest(Request requestUpdate)
        {
            return _requestRepository.Update(requestUpdate);
        }

        public List<Request> GetRequests()
        {
            return _requestRepository.ReadAll().ToList();
        }
    }
}