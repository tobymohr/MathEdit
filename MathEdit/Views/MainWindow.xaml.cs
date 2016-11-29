using System.Windows;
using MathEdit.ViewModels;
using MathEdit.Models;
using MathEdit.Helpers;

namespace MathEdit
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            MainWindowModel modeltest = new MainWindowModel();
            DataContext = new MainWindowModel();
            InitializeComponent();
            //modeltest.openDoc(null);
            //FirstBox.Document = modeltest.MainFlowDocument;
        }
    }
}
