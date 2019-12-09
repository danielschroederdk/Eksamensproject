using System;

namespace EksamensProject.Core.Entity
{
    public class Testimonial
    {
        public int Id { get; set; }
        public User User { get; set; }
        public String TestimonialHeader { get; set; }
        public String TestimonialBody { get; set; }
    }
}