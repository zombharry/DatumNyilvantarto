namespace ViewModel
{
    public class OsztalyViewModel
    {
        public int Id { get; set; }
        public string MegjelenitettNev { get; set; }   // pl. "3/A"
        public string OsztalyFonok { get; set; }
        public bool Figyelmeztet { get; set; } // kell-e figyelmeztetés
    }
}
