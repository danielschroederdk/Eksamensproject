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
    public class StylesController : ControllerBase
    {
        private readonly IStyleService _styleService;

        public StylesController(IStyleService styleService)
        {
            _styleService = styleService;
        }
        // GET api/values
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult<IEnumerable<Style>> Get()
        {
            try
            {
                return _styleService.GetStyles();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        
    }
}