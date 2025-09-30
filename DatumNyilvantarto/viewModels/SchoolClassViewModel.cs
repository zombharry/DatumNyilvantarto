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
            set {
                _grade = value;
                OnPropertyChanged(nameof(Grade));
                OnPropertyChanged(nameof(ClassName));
                OnPropertyChanged(nameof(IsValid));
                if (IsNew)
                {
                    IsNew = false;
                    FirstEditStarted?.Invoke(this, EventArgs.Empty);
                    OnPropertyChanged(nameof(IsNew));
                }
            }
        }

        private string? _letter;
        public string? Letter
        {
            get => _letter;
            set {
                _letter = value;
                OnPropertyChanged(nameof(Letter));
                OnPropertyChanged(nameof(ClassName));
                OnPropertyChanged(nameof(IsValid));
                if (IsNew)
                {
                    IsNew = false;
                    FirstEditStarted?.Invoke(this, EventArgs.Empty);
                    OnPropertyChanged(nameof(IsNew));
                }
            }
        }

        private int _startingYear;
        public int StartingYear
        {
            get => _startingYear;
            set { 
                _startingYear = value; 
                OnPropertyChanged(nameof(StartingYear));
                OnPropertyChanged(nameof(IsValid));
                if (IsNew)
                {
                    IsNew = false;
                    FirstEditStarted?.Invoke(this, EventArgs.Empty);
                    OnPropertyChanged(nameof(IsNew));
                }
            }
        }

        private string? _headTeacher;
        public string? HeadTeacher
        {
            get => _headTeacher;
            set { 
                _headTeacher = value; 
                OnPropertyChanged(nameof(HeadTeacher));
                OnPropertyChanged(nameof(IsValid));
                if (IsNew)
                {
                    IsNew = false;
                    FirstEditStarted?.Invoke(this, EventArgs.Empty);
                    OnPropertyChanged(nameof(IsNew));
                }
            }
        }
        public bool Warning { get; set; } // kell-e figyelmeztetés

        public string ClassName => $"{Grade}/{Letter}";

        public bool IsNew { get; set; }
        public bool IsValid
        {
            get =>
                int.IsPositive(Grade) &&
                StartingYear > 0 &&
                !string.IsNullOrWhiteSpace(HeadTeacher) &&
                !string.IsNullOrWhiteSpace(Letter);
            
        }


        public ObservableCollection<StudentViewModel> Students { get; set; } = new();

        public event EventHandler? FirstEditStarted;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
