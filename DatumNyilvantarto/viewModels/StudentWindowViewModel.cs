using Data;
using DatumNyilvantarto.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DatumNyilvantarto.viewModels
{
    public class StudentWindowViewModel 
    {
        private SchoolClassViewModel _schoolClassViewModel;
        public ObservableCollection<StudentViewModel> Students { get; set; }

        private StudentViewModel? _selectedStudent;
        public StudentViewModel? SelectedStudent
        {
            get => _selectedStudent;
            set
            {
                _selectedStudent = value;
                //LoadStudents();
            }
        }

        public ICommand SaveCommand => new RelayCommand(SaveAll);
        public ICommand DeleteCommand => new RelayCommand(Delete);

        private void Delete(object obj)
        {
            if (obj is StudentViewModel studentVm)
            {
                if (MessageBox.Show($"Biztosan törlöd a(z) {studentVm.Name} tanulót?",
                        "Megerősítés",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    if (studentVm.Id > 0)
                    {
                        var exePath = AppDomain.CurrentDomain.BaseDirectory;
                        var dbPath = Path.Combine(exePath, "schoolDb.db");

                        var options = new DbContextOptionsBuilder<SchoolDbContext>()
                            .UseSqlite($"Data Source={dbPath}")
                            .Options;

                        using var db = new SchoolDbContext(options);
                        var entity = db.Students.FirstOrDefault(s => s.Id == studentVm.Id);
                        if (entity != null)
                        {
                            _ = db.Students.Remove(entity);
                            db.SaveChanges();
                        }
                    }

                    Students.Remove(studentVm);
                    LoadStudents();
                }
            }
        }

        public StudentWindowViewModel(SchoolClassViewModel schoolClassViewModel)
        {
             _schoolClassViewModel = schoolClassViewModel;

            Students = _schoolClassViewModel.Students;

            LoadStudents();
        }

        private void SaveAll(object obj)
        {
            var exePath = AppDomain.CurrentDomain.BaseDirectory;
            var dbPath = Path.Combine(exePath, "schoolDb.db");

            var options = new DbContextOptionsBuilder<SchoolDbContext>()
                .UseSqlite($"Data Source={dbPath}")
                .Options;

            using var db = new SchoolDbContext(options);
            foreach (var studentVm in Students)
            {

                if (studentVm.Id == 0)
                {
                    if (studentVm.Name != string.Empty
                        && studentVm.CityOfBirth != string.Empty
                        && studentVm.DateOfBirth is not null
                        && studentVm.MotherName != string.Empty)
                    {
                        var student = new Student
                        {
                            Name = studentVm.Name,
                            CityOfBirth = studentVm.CityOfBirth,
                            DateOfBirth = studentVm.DateOfBirth,
                            MotherName = studentVm.MotherName,
                            ClassId = _schoolClassViewModel.Id,
                            ChildProtection = studentVm.ChildProtection,
                            Disadvantaged = studentVm.Disadvantaged,
                            SeverlyDisadvantaged = studentVm.SeverlyDisadvantaged
                        };
                        db.Students.Add(student);
                    }

                }
                else
                {
                    var existing = db.Students.FirstOrDefault(o => o.Id == studentVm.Id);
                    if (existing != null)
                    {
                        existing.Name = studentVm.Name;
                        existing.MotherName = studentVm.MotherName;
                        existing.CityOfBirth = studentVm.CityOfBirth;
                        existing.DateOfBirth = studentVm.DateOfBirth;
                        existing.ChildProtection = studentVm.ChildProtection;
                        existing.Disadvantaged = studentVm.Disadvantaged;
                        existing.SeverlyDisadvantaged= studentVm.SeverlyDisadvantaged;

                        db.Students.Update(existing);
                    }

                }
            }
            db.SaveChanges();
            //UndoManager.SetSavePoint();
            LoadStudents();
        }

        private void LoadStudents()
        {
            Students.Clear();
            var exePath = AppDomain.CurrentDomain.BaseDirectory;
            var dbPath = Path.Combine(exePath, "schoolDb.db");

            var options = new DbContextOptionsBuilder<SchoolDbContext>()
                .UseSqlite($"Data Source={dbPath}")
                .Options;

            using var db = new SchoolDbContext(options);
            var studentList = db.Students.Where(t => t.ClassId == _schoolClassViewModel.Id).ToList();
            foreach (var t in studentList)
            {

                Students.Add(new StudentViewModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    MotherName = t.MotherName,
                    CityOfBirth = t.CityOfBirth,
                    DateOfBirth = t.DateOfBirth,
                    ChildProtection = t.ChildProtection,
                    Disadvantaged = t.Disadvantaged,
                    SeverlyDisadvantaged = t.SeverlyDisadvantaged
                });
            }
        }

        private void EnsureDummyRow()
        {
            if (!Students.Any(c => c.IsNew))
            {
                var dummy = new StudentViewModel
                {
                    Id = 0,
                    Name = string.Empty,
                    CityOfBirth = string.Empty,
                    MotherName = string.Empty,
                    ClassId = _schoolClassViewModel.Id,
                    ClassName = _schoolClassViewModel.ClassName,
                    ChildProtection = null,
                    Disadvantaged = null,
                    SeverlyDisadvantaged = null,
                    IsNew = true,
                };
                dummy.FirstEditStarted += Dummy_FirstEditStarted;
                Students.Add(dummy);

            }
        }
        private void Dummy_FirstEditStarted(object? sender, EventArgs e)
        {
            EnsureDummyRow();
        }

    }
}
