using System;
using System.Collections.Generic;
using EksamensProject.Core.ApplicationService;
using EksamensProject.Core.Entity;
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
        public ActionResult<IEnumerable<Request>> Get()
        {
            try
            {
                return _requestService.GetRequests();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Request> Get(int id)
        {
            try
            {
               return Ok(_requestService.FindRequestById(id));
            }
            catch (Exception e)
            {
                return StatusCode(404, $"Request {id} not found");
            }
        }

        // POST api/values
        [HttpPost]
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
        public ActionResult<Request> Put(int id, [FromBody] Request request)
        {
            if (id != request.Id)
                return BadRequest("ID does not coincide");
            
            return Ok(_requestService.UpdateRequest(request));
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult<Request> Delete(int id)
        {
            var toRemove = _requestService.Delete(id);
            return toRemove == null ? StatusCode(404, $"Request {id}  not found") : Ok($"Request: (ID: {id}) deleted");
        }
    }
}