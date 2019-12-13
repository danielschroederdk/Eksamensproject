using System.Linq;
using EksamensProject.Core.ApplicationService;
using EksamensProject.Core.DomainService;
using EksamensProject.Core.Entity;
using EksamensProjectRestApi.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace EksamensProjectRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IUserRepository _repository;
        private IAuthenticationService _authenticationService;

        public AuthenticationController(IUserRepository repos, IAuthenticationService authService)
        {
            _repository = repos;
            _authenticationService = authService;        
        }
        
        // POST api/values
        [HttpPost]
        public ActionResult<User> Post([FromBody]LoginDTO model)
        {
            var user = _repository.ReadAll().FirstOrDefault(u => u.Name == model.Username);

            // check if username exists
            if (user == null)
                return Unauthorized();

            // check if password is correct
            if (!_authenticationService.VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
                return Unauthorized();

            // Authentication successful
            return Ok(new
            {
                username = user.Name,
                token = _authenticationService.GenerateToken(user),
                isAdmin = user.Role
            });
        }

    }
}