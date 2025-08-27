using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Tanulo
    {
        public int Id { get; set; }
        public string Nev { get; set; }

        public int OsztalyId { get; set; }
        public Osztaly Osztaly { get; set; }

        public DateTime GyermekVedelmi { get; set; }
        public DateTime Hatranyos { get; set; }
        public DateTime HalmozottanHatranyos { get; set; }
    }
}
