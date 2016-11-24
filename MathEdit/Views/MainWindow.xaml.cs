using System.Windows;
using MathEdit.ViewModels;
using MathEdit.Models;

namespace MathEdit
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = new MainWindowModel();
            InitializeComponent();
            //textBoxMain.Document = new EnabledFlowDocument();
        }
    }
}
