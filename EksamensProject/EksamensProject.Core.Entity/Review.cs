using System;

namespace EksamensProject.Core.Entity
{
    public class Review
    {
        public User User { get; set; }
        public String ReviewHeader { get; set; }
        public String ReviewBody { get; set; }
    }
}