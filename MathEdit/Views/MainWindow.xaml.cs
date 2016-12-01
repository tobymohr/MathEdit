using System.Windows;
using MathEdit.ViewModels;
using MathEdit.Models;
using MathEdit.Helpers;

namespace MathEdit.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = new MainWindowModel();
            InitializeComponent();


            //MainWindowModel modeltest = new MainWindowModel();
            //modeltest.openDoc(null);

        }
    }
}
