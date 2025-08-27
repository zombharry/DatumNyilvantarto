using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Data;

namespace ViewModel
{
    public class MainViewModel
    {
        public ObservableCollection<OsztalyViewModel> Osztalyok { get; set; }
        public ObservableCollection<Tanulo> Tanulok { get; set; }

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

        public ICommand AddOsztalyCommand { get; }
        public ICommand DeleteOsztalyCommand { get; }
        public ICommand AddTanuloCommand { get; }
        public ICommand DeleteTanuloCommand { get; }

        public MainViewModel()
        {
            Osztalyok = new ObservableCollection<OsztalyViewModel>();
            Tanulok = new ObservableCollection<Tanulo>();

            AddOsztalyCommand = new RelayCommand(AddOsztaly);
            DeleteOsztalyCommand = new RelayCommand(DeleteOsztaly);
            AddTanuloCommand = new RelayCommand(AddTanulo);
            DeleteTanuloCommand = new RelayCommand(DeleteTanulo);
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
                int mostaniEv = DateTime.Now.Year;
                int evfolyam = mostaniEv - osztaly.KezdoEv + 1;
                string megjelenitettNev = $"{evfolyam}/{osztaly.Betu}";

                bool figyelmeztet = osztaly.Tanulok.Any(t =>
                    IsExpiringOrExpired(t.GyermekVedelmi) ||
                    IsExpiringOrExpired(t.Hatranyos) ||
                    IsExpiringOrExpired(t.HalmozottanHatranyos)
                );
                Osztalyok.Add(new OsztalyViewModel
                {
                    Id = osztaly.Id,
                    MegjelenitettNev = megjelenitettNev,
                    OsztalyFonok = osztaly.OsztalyFonok,
                    Figyelmeztet = figyelmeztet
                });
            }
        }

        private void DeleteOsztaly(object obj)
        {
            if (KivalasztottOsztaly != null)
            {
                using var db = new IskolaDbContext();
                db.Osztalyok.Remove(db.Osztalyok.Find(KivalasztottOsztaly.Id));
                db.SaveChanges();
                OsztalyokBetoltese();
            }
        }

        private void AddOsztaly(object obj)
        {
            using var db = new IskolaDbContext();
            var ujOsztaly = new Osztaly
            {
                Betu = "A",
                KezdoEv =2025,
                OsztalyFonok = "Új Tanár",
                EvFolyam = 1,
            };
        }
        private bool IsExpiringOrExpired(DateTime date)
        {
            DateTime today = DateTime.Today;
            return date < today || (date - today).TotalDays <= 14;
        }

    }
}
