using System;

namespace EksamensProject.Core.Entity
{
    public class Composition
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public DateTime Year { get; set; }
        public Double Duration { get; set; } //Duration giver måske ikke mening at seede, da Typescript selv kan finde Duration
        public Tempo Tempo { get; set; }
        public Style Style { get; set; }
        public String URL { get; set; }
        public String PictureURL { get; set; }
    }
}