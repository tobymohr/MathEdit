using System.Windows;
using MathEdit.Views;
using MathEdit.ViewModels;

namespace MathEdit
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = new MainWindowModel();
            InitializeComponent();
            textBoxMain.Document = new EnabledFlowDocument();
        }
    }
}
