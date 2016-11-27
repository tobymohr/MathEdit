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
            FirstBox.Document = new EnabledFlowDocument();
            //modeltest.openDoc(null);
            //FirstBox.Document = modeltest.MainFlowDocument;

            
            

        }
    }
}
