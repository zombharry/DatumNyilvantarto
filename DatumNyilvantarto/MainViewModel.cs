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
using ViewModel.UndoAbleCommand;
using DatumNyilvantarto;

namespace ViewModel
{
    public class MainViewModel
    {
        public ObservableCollection<OsztalyViewModel> Osztalyok { get; set; }
        public ObservableCollection<Tanulo> Tanulok { get; set; }

        public UndoRedoManager UndoManager { get; } = new();

        private Osztaly _kivalasztottOsztaly;
        public Osztaly KivalasztottOsztaly 
        {
            get => _kivalasztottOsztaly;
            set 
            {
                _kivalasztottOsztaly = value;
                TanulokBetoltese();
            }
        }

        public Tanulo KivalasztottTanulo { get; set; }

        public ICommand DeleteOsztalyCommand => new RelayCommand(DeleteOsztaly);
        public ICommand AddTanuloCommand => new RelayCommand(AddTanulo);
        public ICommand DeleteTanuloCommand  => new RelayCommand(DeleteTanulo);

        public ICommand SaveCommand => new RelayCommand(SaveAll);
        public ICommand UndoCommand => new RelayCommand(_ => UndoManager.Undo());
        public ICommand RedoCommand => new RelayCommand(_ => UndoManager.Redo());

        public ICommand TanulokMegnyitCommand => new RelayCommand(TanulokMegnyitasa);

        private void TanulokMegnyitasa(object obj)
        {
            if (obj is OsztalyViewModel osztalyVm)
            {
                var window = new TanuloListaWindow
                {
                    DataContext = osztalyVm
                };
                window.ShowDialog();
            }
        }

        public MainViewModel()
        {
            Osztalyok = new ObservableCollection<OsztalyViewModel>();
            Tanulok = new ObservableCollection<Tanulo>();

            OsztalyokBetoltese();
        }

        private void TanulokBetoltese()
        {
            Tanulok.Clear();
            if (KivalasztottOsztaly != null)
            {
                using var db = new IskolaDbContext();
                var lista = db.Tanulok.Where(t => t.OsztalyId == KivalasztottOsztaly.Id).ToList();
                foreach (var t in lista)
                {
                    Tanulok.Add(t);
                }
            }

        }

        private void DeleteTanulo(object obj)
        {
            if (KivalasztottTanulo != null)
            {
                using var db = new IskolaDbContext();
                db.Tanulok.Remove(db.Tanulok.Find(KivalasztottTanulo.Id));
                db.SaveChanges();
                TanulokBetoltese();
            }
        }

        private void AddTanulo(object obj)
        {
            if (KivalasztottOsztaly != null)
            {
                using var db = new IskolaDbContext();

                var tanulo = new Tanulo
                {
                    Nev = "Placeholder",
                    OsztalyId = KivalasztottOsztaly.Id,
                    GyermekVedelmi = System.DateTime.Now.AddDays(10),
                    Hatranyos = System.DateTime.Now.AddDays(20),
                    HalmozottanHatranyos = System.DateTime.Now.AddDays(30)
                };
                db.Tanulok.Add(tanulo);
                db.SaveChanges();
                TanulokBetoltese();
            }
        }

        private void OsztalyokBetoltese()
        {
            using var db = new IskolaDbContext();
            Osztalyok.Clear();
            var list = db.Osztalyok.Include(c => c.Tanulok).ToList();
            foreach (var osztaly in list)
            {
                bool figyelmeztet = osztaly.Tanulok.Any(t =>
                    IsExpiringOrExpired(t.GyermekVedelmi) ||
                    IsExpiringOrExpired(t.Hatranyos) ||
                    IsExpiringOrExpired(t.HalmozottanHatranyos)
                );
                Osztalyok.Add(new OsztalyViewModel
                {
                    Id = osztaly.Id,
                    Evfolyam = osztaly.EvFolyam,
                    Betu = osztaly.Betu,
                    KezdoEv = osztaly.KezdoEv,
                    OsztalyFonok = osztaly.OsztalyFonok,
                    Figyelmeztet = figyelmeztet
                });
            }
        }

        private void DeleteOsztaly(object obj)
        {
            if (obj is OsztalyViewModel osztalyVm)
            {
                if (System.Windows.MessageBox.Show($"Biztosan törlöd a(z) {osztalyVm.MegjelenitettNev} osztályt?",
                        "Megerősítés",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    if (osztalyVm.Id > 0)
                    {
                        using var db = new IskolaDbContext();
                        var entity = db.Osztalyok.Find(osztalyVm.Id);
                        db.Osztalyok.Remove(db.Osztalyok.Find(osztalyVm.Id));
                        db.SaveChanges();
                        
                    }
                    

                    Osztalyok.Remove(osztalyVm);
                    OsztalyokBetoltese();
                }
                
            }
        }

        private void SaveAll(object obj) 
        {
            using var db = new IskolaDbContext();
            foreach (var osztalyVM in Osztalyok)
            {
                
                if (osztalyVM.Id == 0)
                {
                    var osztaly = new Osztaly
                    {
                        Betu = osztalyVM.Betu,
                        EvFolyam = osztalyVM.Evfolyam,
                        KezdoEv = osztalyVM.KezdoEv,
                        OsztalyFonok = osztalyVM.OsztalyFonok,
                    };
                    db.Osztalyok.Add(osztaly);
                }
                else
                {
                    var letezo = db.Osztalyok.FirstOrDefault(o => o.Id == osztalyVM.Id);
                    if ( letezo != null)
                    {
                        letezo.Betu = osztalyVM.Betu;
                        letezo.EvFolyam = osztalyVM.Evfolyam;
                        letezo.KezdoEv = osztalyVM.KezdoEv;
                        letezo.OsztalyFonok = osztalyVM.OsztalyFonok;

                        db.Osztalyok.Update(letezo);
                    }
                   
                }
            }
            db.SaveChanges();
            UndoManager.SetSavePoint();
            OsztalyokBetoltese();
        }
        private bool IsExpiringOrExpired(DateTime date)
        {
            DateTime today = DateTime.Today;
            return date < today || (date - today).TotalDays <= 14;
        }

    }
}
