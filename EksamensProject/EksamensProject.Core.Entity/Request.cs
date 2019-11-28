using System;

namespace EksamensProject.Core.Entity
{
    public class Request
    {
        public User User { get; set; }
        public String RequestHeader { get; set; }
        public String RequestBody { get; set; }
        
        
    }
}