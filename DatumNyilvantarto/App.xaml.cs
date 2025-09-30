using Data;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Windows;

namespace DatumNyilvantarto;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        using (var db = new SchoolDbContextFactory().CreateDbContext(Array.Empty<string>()))
        {
            db.Database.Migrate(); // létrehozza a táblákat és lefuttatja a migrációkat
        }

        AppDomain.CurrentDomain.UnhandledException += (s, args) =>
        {
            MessageBox.Show(args.ExceptionObject.ToString(), "Unhandled Exception");
        };

        base.OnStartup(e);

    }

}
