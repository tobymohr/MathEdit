using MathEdit.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MathEdit
{
    public partial class MainWindow : Window
    {
        EnabledFlowDocument fd = new EnabledFlowDocument();
    
        public MainWindow()
        {
            InitializeComponent();
          
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Add_Click(object sender, RoutedEventArgs e)
        {
            createNewRtb();
        }

       

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void createNewRtb()
        {
            Paragraph para = new Paragraph();
            fd.Blocks.Add(para);
            textBoxMain.Document = fd;
            RichTextBox rtb = new RichTextBox() { Focusable = true };
            para.Inlines.Add(rtb);
        }

        private void Control1_MouseEnter(Object sender, RoutedEventArgs e)
        {
            
            RichTextBox rt = (RichTextBox)sender;
            rt.Focus();
            Console.WriteLine("SOO GUT");
        }


    }
}
