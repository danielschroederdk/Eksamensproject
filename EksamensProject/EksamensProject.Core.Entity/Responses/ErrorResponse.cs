using System.Collections.Generic;

namespace EksamensProject.Core.Entity
{
    public class ErrorResponse
    {
        public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
            
        
    }
}