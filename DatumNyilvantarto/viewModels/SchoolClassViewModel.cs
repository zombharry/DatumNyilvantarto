using DatumNyilvantarto;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DatumNyilvantarto.viewModels
{
    public class SchoolClassViewModel : INotifyPropertyChanged
    {
        public int Id { get; set; }

        private int _grade;
        public int Grade
        {
            get => _grade;
            set { _grade = value; OnPropertyChanged(nameof(Grade)); OnPropertyChanged(nameof(ClassName)); }
        }

        private string _letter;
        public string Letter
        {
            get => _letter;
            set { _letter = value; OnPropertyChanged(nameof(Letter)); OnPropertyChanged(nameof(ClassName)); }
        }

        private int _startingYear;
        public int StartingYear
        {
            get => _startingYear;
            set { _startingYear = value; OnPropertyChanged(nameof(StartingYear)); OnPropertyChanged(nameof(StartingYear)); }
        }

        private string _headTeacher;
        public string HeadTeacher
        {
            get => _headTeacher;
            set { _headTeacher = value; OnPropertyChanged(nameof(HeadTeacher)); }
        }
        public bool Warning { get; set; } // kell-e figyelmeztetés

        public string ClassName => $"{Grade}/{Letter}";

        public ObservableCollection<StudentViewModel> Students { get; set; } = new();

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
