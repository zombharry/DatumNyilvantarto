using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows;
using Data;
using DatumNyilvantarto;
using DatumNyilvantarto.Utils.UndoAbleCommand;
using DatumNyilvantarto.Utils;
using System.IO;

namespace DatumNyilvantarto.viewModels
{
    public class MainViewModel
    {
        public ObservableCollection<SchoolClassViewModel> Classes { get; set; }
        public ObservableCollection<Student> Students { get; set; }

        public UndoRedoManager UndoManager { get; } = new();

        private SchoolClass _selectedClass;
        public SchoolClass SelectedClass 
        {
            get => _selectedClass;
            set 
            {
                _selectedClass = value;
                LoadStudents();
            }
        }

        public Student SelectedStudent { get; set; }

        public ICommand DeleteSchoolClassCommand => new RelayCommand(DeleteSchoolClass);
        public ICommand AddStudentCommand => new RelayCommand(AddStudent);
        public ICommand DeleteStudentCommand  => new RelayCommand(DeleteStudent);

        public ICommand SaveCommand => new RelayCommand(SaveAll);
        public ICommand UndoCommand => new RelayCommand(_ => UndoManager.Undo());
        public ICommand RedoCommand => new RelayCommand(_ => UndoManager.Redo());

        public ICommand OpenStudentsCommand => new RelayCommand(OpenStudents);

        private void OpenStudents(object obj)
        {
            if (obj is SchoolClassViewModel schoolClassVm)
            {
                var window = new TanuloListaWindow
                {
                    DataContext = schoolClassVm
                };
                window.ShowDialog();
            }
        }

        public MainViewModel()
        {
            Classes = new ObservableCollection<SchoolClassViewModel>();
            Students = new ObservableCollection<Student>();

            LoadClasses();
        }

        private void LoadStudents()
        {
            Students.Clear();
            if (SelectedClass != null)
            {
                var exePath = AppDomain.CurrentDomain.BaseDirectory;
                var dbPath = Path.Combine(exePath, "schoolDb.db");

                var options = new DbContextOptionsBuilder<SchoolDbContext>()
                    .UseSqlite($"Data Source={dbPath}")
                    .Options;

                using var db = new SchoolDbContext(options);
                var studentList = db.Students.Where(t => t.ClassId == SelectedClass.Id).ToList();
                foreach (var t in studentList)
                {
                    Students.Add(t);
                }
            }

        }

        private void DeleteStudent(object obj)
        {
            if (SelectedStudent != null)
            {
                var exePath = AppDomain.CurrentDomain.BaseDirectory;
                var dbPath = Path.Combine(exePath, "schoolDb.db");

                var options = new DbContextOptionsBuilder<SchoolDbContext>()
                    .UseSqlite($"Data Source={dbPath}")
                    .Options;

                using var db = new SchoolDbContext(options);
                db.Students.Remove(db.Students.Find(SelectedStudent.Id));
                db.SaveChanges();
                LoadStudents();
            }
        }

        private void AddStudent(object obj)
        {
            if (SelectedClass != null)
            {
                var exePath = AppDomain.CurrentDomain.BaseDirectory;
                var dbPath = Path.Combine(exePath, "schoolDb.db");

                var options = new DbContextOptionsBuilder<SchoolDbContext>()
                    .UseSqlite($"Data Source={dbPath}")
                    .Options;

                using var db = new SchoolDbContext(options);

                var tanulo = new Student
                {
                    Name = "Placeholder",
                    CityOfBirth = "Placeholder",
                    DateOfBirth = DateTime.Now,
                    MotherName = "Placeholder",
                    ClassId = SelectedClass.Id,
                    ChildProtection = DateTime.Now.AddDays(10),
                    Disadvantaged = DateTime.Now.AddDays(20),
                    SeverlyDisadvantaged = DateTime.Now.AddDays(30)
                };
                db.Students.Add(tanulo);
                db.SaveChanges();
                LoadStudents();
            }
        }

        private void LoadClasses()
        {
            var exePath = AppDomain.CurrentDomain.BaseDirectory;
            var dbPath = Path.Combine(exePath, "schoolDb.db");

            var options = new DbContextOptionsBuilder<SchoolDbContext>()
                .UseSqlite($"Data Source={dbPath}")
                .Options;

            using var db = new SchoolDbContext(options);
            Classes.Clear();
            var classes = db.SchoolClasses.Include(c => c.Students).ToList();
            
            var list = db.SchoolClasses.Include(s => s.Students).ToList();
            foreach (var schoolClass in list)
            {
                bool warn = schoolClass.Students.Any(t =>
                    IsExpiringOrExpired(t.ChildProtection) ||
                    IsExpiringOrExpired(t.Disadvantaged) ||
                    IsExpiringOrExpired(t.SeverlyDisadvantaged)
                );
                Classes.Add(new SchoolClassViewModel
                {
                    Id = schoolClass.Id,
                    Grade = schoolClass.Grade,
                    Letter = schoolClass.Letter,
                    StartingYear = schoolClass.StartingYear,
                    HeadTeacher = schoolClass.HeadTeacher,
                    Warning = warn
                });
            }
        }

        private void DeleteSchoolClass(object obj)
        {
            if (obj is SchoolClassViewModel schoolClassVm)
            {
                if (MessageBox.Show($"Biztosan törlöd a(z) {schoolClassVm.ClassName} osztályt?",
                        "Megerősítés",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    if (schoolClassVm.Id > 0)
                    {
                        var exePath = AppDomain.CurrentDomain.BaseDirectory;
                        var dbPath = Path.Combine(exePath, "schoolDb.db");

                        var options = new DbContextOptionsBuilder<SchoolDbContext>()
                            .UseSqlite($"Data Source={dbPath}")
                            .Options;

                        using var db = new SchoolDbContext(options);
                        var entity = db.SchoolClasses.Find(schoolClassVm.Id);
                        db.SchoolClasses.Remove(db.SchoolClasses.Find(schoolClassVm.Id));
                        db.SaveChanges();
                        
                    }
                    

                    Classes.Remove(schoolClassVm);
                    LoadClasses();
                }
                
            }
        }

        private void SaveAll(object obj) 
        {
            var exePath = AppDomain.CurrentDomain.BaseDirectory;
            var dbPath = Path.Combine(exePath, "schoolDb.db");

            var options = new DbContextOptionsBuilder<SchoolDbContext>()
                .UseSqlite($"Data Source={dbPath}")
                .Options;

            using var db = new SchoolDbContext(options);
            foreach (var schoolClassVm in Classes)
            {
                
                if (schoolClassVm.Id == 0)
                {
                    var schoolClass = new SchoolClass
                    {
                        Letter = schoolClassVm.Letter,
                        Grade = schoolClassVm.Grade,
                        StartingYear = schoolClassVm.StartingYear,
                        HeadTeacher = schoolClassVm.HeadTeacher,
                    };
                    db.SchoolClasses.Add(schoolClass);
                }
                else
                {
                    var existing = db.SchoolClasses.FirstOrDefault(o => o.Id == schoolClassVm.Id);
                    if (existing != null)
                    {
                        existing.Letter = schoolClassVm.Letter;
                        existing.Grade = schoolClassVm.Grade;
                        existing.StartingYear = schoolClassVm.StartingYear;
                        existing.HeadTeacher = schoolClassVm.HeadTeacher;

                        db.SchoolClasses.Update(existing);
                    }
                   
                }
            }
            db.SaveChanges();
            UndoManager.SetSavePoint();
            LoadClasses();
        }
        private bool IsExpiringOrExpired(DateTime date)
        {
            DateTime today = DateTime.Today;
            return date < today || (date - today).TotalDays <= 14;
        }

    }
}
