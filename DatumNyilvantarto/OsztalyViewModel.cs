using DatumNyilvantarto;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ViewModel
{
    public class OsztalyViewModel : INotifyPropertyChanged
    {
        public int Id { get; set; }

        private int _evfolyam;
        public int Evfolyam
        {
            get => _evfolyam;
            set { _evfolyam = value; OnPropertyChanged(nameof(Evfolyam)); OnPropertyChanged(nameof(MegjelenitettNev)); }
        }

        private string _betu;
        public string Betu
        {
            get => _betu;
            set { _betu = value; OnPropertyChanged(nameof(Betu)); OnPropertyChanged(nameof(MegjelenitettNev)); }
        }

        private int _kezdoEv;
        public int KezdoEv
        {
            get => _kezdoEv;
            set { _kezdoEv = value; OnPropertyChanged(nameof(KezdoEv)); OnPropertyChanged(nameof(KezdoEv)); }
        }

        private string _osztalyFonok;
        public string OsztalyFonok
        {
            get => _osztalyFonok;
            set { _osztalyFonok = value; OnPropertyChanged(nameof(OsztalyFonok)); }
        }
        public bool Figyelmeztet { get; set; } // kell-e figyelmeztetés

        public string MegjelenitettNev => $"{Evfolyam}/{Betu}";

        public ObservableCollection<TanuloViewModel> Diakok { get; set; } = new();

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
