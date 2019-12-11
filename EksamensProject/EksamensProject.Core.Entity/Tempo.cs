namespace EksamensProject.Core.Entity
{
    public class Tempo
    {
        public int Id { get; set; }
        public int BeatsPerMinute { get; set; }
        public TempoMarking TempoMarking { get; set; }
        public TimeSignature TimeSignature { get; set; }
    }
}