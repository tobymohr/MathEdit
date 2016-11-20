using System.Windows;
using MathEdit.Views;
using MathEdit.ViewModels;

namespace MathEdit
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowModel();
            textBoxMain.Document = new EnabledFlowDocument();
        }
    }
}
