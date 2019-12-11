using System;
using EksamensProject.Core.Entity;

namespace EksamensProjectRestApi.DTOs
{
    public class TestimonialDTO
    {
        public String UserName { get; set; }
        public String TestimonialHeader { get; set; }
        public String TestimonialBody { get; set; }
    }
}