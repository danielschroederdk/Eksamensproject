using System;

namespace EksamensProject.Core.Entity
{
    public class Composition
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Year { get; set; }
        public Double Duration { get; set; } 
        public Tempo Tempo { get; set; }
        public Style Style { get; set; }
        public String Comment { get; set; }
        public String URL { get; set; }
        public String PictureURL { get; set; }
    }
}