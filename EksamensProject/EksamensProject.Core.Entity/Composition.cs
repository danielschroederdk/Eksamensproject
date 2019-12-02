using System;

namespace EksamensProject.Core.Entity
{
    public class Composition
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public DateTime Year { get; set; }
        public Double Duration { get; set; }
        public Tempo Tempo { get; set; }
        public Style Style { get; set; }
        
    }
}