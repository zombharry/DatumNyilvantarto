namespace Data
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    
    public class SchoolClass
    {
        public int Id { get; set; }
        public string? Letter { get; set; }       // pl. "A", "B"
        public int Grade { get; set; }
        public int StartingYear { get; set; }     // osztály indulási éve
        public string? HeadTeacher { get; set; }

        public ICollection<Student>? Students { get; set; }
    }
}
