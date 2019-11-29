using System;

namespace EksamensProject.Core.Entity
{
    public class Request
    {

        public int Id { get; set; } 
        public User User { get; set; }
        public String RequestHeader { get; set; }
        public String RequestBody { get; set; }
        
        
    }
}