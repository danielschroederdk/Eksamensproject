using System;

namespace EksamensProjectRestApi.DTOs
{
    public class RequestDTO
    {
        public int Id { get; set; } 
        public int UserId { get; set; }
        public String RequestHeader { get; set; }
        public String RequestBody { get; set; }
    }
}