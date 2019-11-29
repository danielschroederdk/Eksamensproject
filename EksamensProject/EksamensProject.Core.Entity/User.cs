using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EksamensProject.Core.Entity
{
    public class User
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
        public List<Review> Reviews { get; set; }
        public List<Request> Requests { get; set; }

    }
}