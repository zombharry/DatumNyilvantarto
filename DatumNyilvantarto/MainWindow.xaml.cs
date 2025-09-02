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

    protected override void OnPreviewKeyDown(KeyEventArgs e)
    {
        if (e.Key == Key.Z)
        {
            if (Keyboard.FocusedElement is TextBox textBox && textBox.CanUndo)
            {
                textBox.Undo();
            }
            else
            {
                (DataContext as MainViewModel)?.UndoCommand.Execute(null);
            }
            e.Handled = true;
        }
        else if (e.Key == Key.Y)
        {
            if (Keyboard.FocusedElement is TextBox textBox && textBox.CanRedo)
            {
                textBox.Redo();
            }
            else
            {
                (DataContext as MainViewModel)?.RedoCommand.Execute(null);
            }
            e.Handled = true;
        }

            base.OnPreviewKeyDown(e);
    }
}