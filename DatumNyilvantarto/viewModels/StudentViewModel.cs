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
        private string _name = string.Empty;
        private string _cityOfBirth = string.Empty;
        private DateTime? _dateOfBirth;
        private string _motherName = string.Empty;
        private DateTime? _childProtection;
        private DateTime? _disadvantaged;
        private DateTime? _severlyDisadvantaged;

        public int Id { get; set; }
        public string Name 
        {
            get => _name;
            set {
                _name = value;
                OnPropertyChanged(nameof(Name));
                if (IsNew)
                {
                    IsNew = false;
                    FirstEditStarted?.Invoke(this, EventArgs.Empty);
                    OnPropertyChanged(nameof(IsNew));
                }
            }
        }

        public string CityOfBirth
        {
            get => _cityOfBirth;
            set { 
                _cityOfBirth = value;
                OnPropertyChanged(nameof(CityOfBirth));
                if (IsNew)
                {
                    IsNew = false;
                    FirstEditStarted?.Invoke(this, EventArgs.Empty);
                    OnPropertyChanged(nameof(IsNew));
                }
            }
        }

        public DateTime? DateOfBirth
        {
            get => _dateOfBirth;
            set { 
                _dateOfBirth = value;
                OnPropertyChanged(nameof(DateOfBirth));
                if (IsNew)
                {
                    IsNew = false;
                    FirstEditStarted?.Invoke(this, EventArgs.Empty);
                    OnPropertyChanged(nameof(IsNew));
                }
            }
        }
        public string MotherName
        {
            get => _motherName;
            set { _motherName = value;
                OnPropertyChanged(nameof(MotherName));
                if (IsNew)
                {
                    IsNew = false;
                    FirstEditStarted?.Invoke(this, EventArgs.Empty);
                    OnPropertyChanged(nameof(IsNew));
                }
            }
        }

        public int ClassId { get; set; }
        public string ClassName { get; set; }

        public DateTime? ChildProtection
        {
            get => _childProtection;
            set { _childProtection = value;
                OnPropertyChanged(nameof(ChildProtection));
                if (IsNew)
                {
                    IsNew = false;
                    FirstEditStarted?.Invoke(this, EventArgs.Empty);
                    OnPropertyChanged(nameof(IsNew));
                }
            }
        }
        public DateTime? Disadvantaged
        {
            get => _disadvantaged;
            set { 
                _disadvantaged = value;
                OnPropertyChanged(nameof(Disadvantaged));
                if (IsNew)
                {
                    IsNew = false;
                    FirstEditStarted?.Invoke(this, EventArgs.Empty);
                    OnPropertyChanged(nameof(IsNew));
                }
            }
        }
        public DateTime? SeverlyDisadvantaged
        {
            get => _severlyDisadvantaged;
            set { 
                _severlyDisadvantaged = value; 
                OnPropertyChanged(nameof(SeverlyDisadvantaged));
                if (IsNew)
                {
                    IsNew = false;
                    FirstEditStarted?.Invoke(this, EventArgs.Empty);
                    OnPropertyChanged(nameof(IsNew));
                }
            }
        }


        public bool IsNew { get; set; }

        public StudentViewModel()
        {
            
        }

        public StudentViewModel(SchoolClassViewModel schoolClassViewModel) {

            ClassId = schoolClassViewModel.Id;
            ClassName = schoolClassViewModel.ClassName;
        }

        public event EventHandler? FirstEditStarted;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
    }
}
