using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatumNyilvantarto.viewModels
{
    public class StudentViewModel: INotifyPropertyChanged
    {
        private int _id;
        private string _name = string.Empty;
        private string _cityOfBirth = string.Empty;
        private DateTime _dateOfBirth;
        private string _motherName = string.Empty;
        private DateTime _childProtection;
        private DateTime _disadvantaged;
        private DateTime _severlyDisadvantaged;

        public int Id {get => _id;}
        public string Name 
        {
            get => _name;
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }

        public string CityOfBirth
        {
            get => _cityOfBirth;
            set { _cityOfBirth = value; OnPropertyChanged(nameof(CityOfBirth)); }
        }

        public DateTime DateOfBirth
        {
            get => _dateOfBirth;
            set { _dateOfBirth = value;OnPropertyChanged(nameof(DateOfBirth)); }
        }

        public string MotherName
        {
            get => _motherName;
            set { _motherName = value; OnPropertyChanged(nameof(MotherName)); }
        }

        public int ClassId { get; set; }
        public string ClassName { get; set; }

        public DateTime ChildProtection
        {
            get => _childProtection;
            set { _childProtection = value; OnPropertyChanged(nameof(ChildProtection)); }
        }
        public DateTime Disadvantaged
        {
            get => _disadvantaged;
            set { _disadvantaged = value; OnPropertyChanged(nameof(Disadvantaged)); }
        }
        public DateTime SeverlyDisadvantaged
        {
            get => _severlyDisadvantaged;
            set { _severlyDisadvantaged = value; OnPropertyChanged(nameof(SeverlyDisadvantaged)); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
