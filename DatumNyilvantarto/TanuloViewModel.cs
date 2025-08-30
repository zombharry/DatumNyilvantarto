using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatumNyilvantarto
{
    public class TanuloViewModel: INotifyPropertyChanged
    {
        private int _id;
        private string _nev = string.Empty;
        private DateTime _gyermekVedelmi;
        private DateTime _hatranyos;
        private DateTime _halmozottanHatranyos;

        public int Id {get;set;}
        public string Nev 
        {
            get => _nev;
            set { _nev = value; OnPropertyChanged(nameof(Nev)); }
        }

        public int OsztalyId { get; set; }
        public string Osztaly { get; set; }

        public DateTime GyermekVedelmi 
        {
            get => _gyermekVedelmi;
            set { _gyermekVedelmi = value; OnPropertyChanged(nameof(GyermekVedelmi)); }
        }
        public DateTime Hatranyos 
        {
            get => _hatranyos;
            set { _hatranyos = value; OnPropertyChanged(nameof(Hatranyos)); }
        }
        public DateTime HalmozottanHatranyos
        {
            get => _halmozottanHatranyos;
            set { _halmozottanHatranyos = value; OnPropertyChanged(nameof(HalmozottanHatranyos)); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
