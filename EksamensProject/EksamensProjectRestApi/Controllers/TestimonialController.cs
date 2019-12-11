using System;
using System.Collections.Generic;
using EksamensProject.Core.ApplicationService;
using EksamensProject.Core.Entity;
using EksamensProjectRestApi.DTOs;
using Microsoft.AspNetCore.Mvc;


namespace EksamensProjectRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestimonialController : ControllerBase
    {
        private readonly ITestimonialService _reviewService;

        public TestimonialController(ITestimonialService reviewService)
        {
            _reviewService = reviewService;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<TestimonialDTO>> Get()
        {
            try
            {
                var list = _reviewService.GetReviews();
                var newList = new List<TestimonialDTO>();

                foreach (var testimonial in list)
                {
                    newList.Add(new TestimonialDTO()
                    {
                        UserName = testimonial.User.Name,
                        TestimonialHeader = testimonial.TestimonialHeader,
                        TestimonialBody = testimonial.TestimonialBody
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
        public ActionResult<Testimonial> Get(int id)
        {
            if (id < 1) return BadRequest("Id must be greater then 0");
            var testimonial = _reviewService.FindReviewById(id);
            
            try
            {
                return Ok(new TestimonialDTO()
               {
                   UserName = testimonial.User.Name,
                   TestimonialHeader = testimonial.TestimonialHeader,
                   TestimonialBody = testimonial.TestimonialBody
               });
            }
            catch (Exception e)
            {
                return StatusCode(404, $"Request {id} not found");
            }
        }

        // POST api/values
        [HttpPost]
        public ActionResult<Testimonial> Post([FromBody] Testimonial review)
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
        public ActionResult<Testimonial> Put(int id, [FromBody] Testimonial review)
        {
            if (id != review.Id)
                return BadRequest("ID does not coincide");
            
            return Ok(_reviewService.UpdateReview(review));
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult<Testimonial> Delete(int id)
        {
            var toRemove = _reviewService.Delete(id);
            return toRemove == null ? StatusCode(404, $"Request {id}  not found") : Ok($"Request: (ID: {id}) deleted");
        }
    }
}