using System;
using EksamensProject.Core.Entity;

namespace EksamensProjectRestApi.DTOs
{
    public class RequestDTO
    {
        public int Id { get; set; } 
        public int userId { get; set; }
        public String RequestHeader { get; set; }
        public String RequestBody { get; set; }
    }
}