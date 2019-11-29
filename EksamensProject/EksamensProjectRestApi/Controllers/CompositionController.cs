using System;
using System.Collections.Generic;
using EksamensProject.Core.ApplicationService;
using EksamensProject.Core.Entity;
using Microsoft.AspNetCore.Mvc;


namespace EksamensProjectRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompositionController : ControllerBase
    {
        private readonly ICompositionService _compositionService;

        public CompositionController(ICompositionService compositionService)
        {
            _compositionService = compositionService;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Composition>> Get()
        {
            try
            {
                return _compositionService.GetCompositions();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Composition> Get(int id)
        {
            try
            {
               return Ok(_compositionService.FindCompositionById(id));
            }
            catch (Exception e)
            {
                return StatusCode(404, $"Composition {id} not found");
            }
        }

        // POST api/values
        [HttpPost]
        public ActionResult<Composition> Post([FromBody] Composition composition)
        {
            try
            {
                return Ok(_compositionService.CreateComposition(composition));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult<Composition> Put(int id, [FromBody] Composition composition)
        {
            if (id != composition.Id)
                return BadRequest("ID does not coincide");
            
            return Ok(_compositionService.UpdateComposition(composition));
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult<Composition> Delete(int id)
        {
            var toRemove = _compositionService.Delete(id);
            return toRemove == null ? StatusCode(404, $"Composition {id}  not found") : Ok($"Composition: {toRemove.Name} (ID: {id}) deleted");
        }
    }
}