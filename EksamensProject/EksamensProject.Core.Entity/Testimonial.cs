using System;

namespace EksamensProject.Core.Entity
{
    public class Testimonial
    {
        public int Id { get; set; }
        public User User { get; set; }
        public String ReviewHeader { get; set; }
        public String ReviewBody { get; set; }
    }
}