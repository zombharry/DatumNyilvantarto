using DatumNyilvantarto.viewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
namespace DatumNyilvantarto;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainViewModel ViewModel { get; set; }
    public MainWindow()
    {
        InitializeComponent();
        ViewModel = new MainViewModel();
        DataContext = ViewModel;
    }

    private void GlobalUndo_CanExecute(object sender, CanExecuteRoutedEventArgs e)
    {
        e.CanExecute = ViewModel?.UndoCommand.CanExecute(null) == true;
        e.Handled = true;
    }

    private void GlobalUndo_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        ViewModel?.UndoCommand.Execute(null);
        e.Handled = true;
    }

    private void GlobalRedo_CanExecute(object sender, CanExecuteRoutedEventArgs e)
    {
        e.CanExecute = ViewModel?.RedoCommand.CanExecute(null) == true;
        e.Handled = true;
    }

    private void GlobalRedo_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        ViewModel?.RedoCommand.Execute(null);
        e.Handled = true;
    }
}