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
    public class RequestController : ControllerBase
    {
        private readonly IRequestService _requestService;

        public RequestController(IRequestService requestService)
        {
            _requestService = requestService;
        }
        // GET api/values
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult<IEnumerable<AllRequestDTO>> Get()
        {
            try
            {
                var list = _requestService.GetRequests();
                var newList = new List<AllRequestDTO>();

                foreach (var request in list)
                {
                    newList.Add(new AllRequestDTO()
                    {
                        Id = request.Id,
                        RequestHeader = request.RequestHeader
                    });
                }
                return Ok(newList);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator")]
        public ActionResult<RequestDTO> Get(int id)
        {
            try
            { 
                var request = _requestService.FindRequestById(id);
                
               return Ok(new RequestDTO()
               {
                   Id = request.Id,
                   userId = request.User.Id,
                   RequestHeader = request.RequestHeader,
                   RequestBody = request.RequestBody
               });
            }
            catch (Exception e)
            {
                return StatusCode(404, $"Request {id} not found");
            }
        }

        // POST api/values
        [HttpPost]
        [Authorize]
        public ActionResult<Request> Post([FromBody] Request request)
        {
            try
            {
                return Ok(_requestService.CreateRequest(request));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        [Authorize]
        public ActionResult<Request> Put(int id, [FromBody] Request request)
        {
            if (id != request.Id)
                return BadRequest("ID does not coincide");
            
            return Ok(_requestService.UpdateRequest(request));
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public ActionResult<Request> Delete(int id)
        {
            var toRemove = _requestService.Delete(id);
            return toRemove == null ? StatusCode(404, $"Request {id}  not found") : Ok($"Request: (ID: {id}) deleted");
        }
    }
}