using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ViewModel;

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