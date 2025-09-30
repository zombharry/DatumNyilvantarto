using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Student
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? CityOfBirth { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? MotherName { get; set; }
        public int ClassId { get; set; }
        public SchoolClass? SchoolClass { get; set; }
        public DateTime? ChildProtection { get; set; }
        public DateTime? Disadvantaged { get; set; }
        public DateTime? SeverlyDisadvantaged { get; set; }
    }
}
