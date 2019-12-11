using System;
using System.Collections.Generic;
using EksamensProject.Core.ApplicationService;
using EksamensProject.Core.Entity;
using EksamensProjectRestApi.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace EksamensProjectRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<AllUsersDTO>> Get()
        {
            try
            {
                var list = _userService.GetUsers();
                var newList = new List<AllUsersDTO>();

                foreach (var user in list)
                {
                    newList.Add(new AllUsersDTO()
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Email = user.Email
                    });
                }
                return newList;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            try
            {
               return Ok(_userService.FindUserById(id));
            }
            catch (Exception e)
            {
                return StatusCode(404, $"owner {id} not found");
            }
        }

        // POST api/values
        [HttpPost]
        public ActionResult<User> Post([FromBody] User user)
        {
            try
            {
                return Ok(_userService.CreateUser(user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult<User> Put(int id, [FromBody] User user)
        {
            if (id != user.Id)
                return BadRequest("ID does not coincide");
            
            return Ok(_userService.UpdateUser(user));
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult<User> Delete(int id)
        {
            var toRemove = _userService.Delete(id);
            return toRemove == null ? StatusCode(404, $"user {id}  not found") : Ok($"User: {toRemove.Name} (ID: {id}) deleted");
        }
    }
}