using System.Windows;
using MathEdit.ViewModels;

namespace MathEdit.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = new MainWindowModel();
            InitializeComponent();
        }
    }
}
