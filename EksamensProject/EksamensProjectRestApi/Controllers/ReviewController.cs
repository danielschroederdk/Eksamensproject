using System;
using System.Collections.Generic;
using EksamensProject.Core.ApplicationService;
using EksamensProject.Core.Entity;
using Microsoft.AspNetCore.Mvc;


namespace EksamensProjectRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Review>> Get()
        {
            try
            {
                return _reviewService.GetReviews();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Review> Get(int id)
        {
            try
            {
               return Ok(_reviewService.FindReviewById(id));
            }
            catch (Exception e)
            {
                return StatusCode(404, $"Request {id} not found");
            }
        }

        // POST api/values
        [HttpPost]
        public ActionResult<Review> Post([FromBody] Review review)
        {
            try
            {
                return Ok(_reviewService.CreateReview(review));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult<Review> Put(int id, [FromBody] Review review)
        {
            if (id != review.Id)
                return BadRequest("ID does not coincide");
            
            return Ok(_reviewService.UpdateReview(review));
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult<Review> Delete(int id)
        {
            var toRemove = _reviewService.Delete(id);
            return toRemove == null ? StatusCode(404, $"Request {id}  not found") : Ok($"Request: (ID: {id}) deleted");
        }
    }
}