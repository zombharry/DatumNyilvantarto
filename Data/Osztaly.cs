namespace Data
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Osztaly
    {
        public int Id { get; set; }
        public string Betu { get; set; }       // pl. "A", "B"
        public int EvFolyam { get; set; }
        public int KezdoEv { get; set; }     // osztály indulási éve
        public string OsztalyFonok { get; set; }

        public ICollection<Tanulo>? Tanulok { get; set; }
    }
}
