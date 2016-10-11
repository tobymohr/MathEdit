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
            if(fd.Blocks.Count == 1)
            {
                para = (Paragraph)fd.Blocks.LastBlock;
            }else
            {
                fd.Blocks.Add(para);
            }
          

            textBoxMain.Document = fd;
            RichTextBox rtb = new RichTextBox() { Focusable = true };
            rtb.Width = 100;
            rtb.Focusable = true;
            rtb.Focus();
            rtb.TextChanged += onTextChanged;
            rtb.AcceptsReturn = false;
            para.Inlines.Add(rtb);
        }

        private void MouseEnter(Object sender, RoutedEventArgs e)
        {
            RichTextBox rt = (RichTextBox)sender;
            rt.Focus();
            Console.WriteLine("Mouse entered" );
        }

        private void onTextChanged(object sender, EventArgs e)
        {
            RichTextBox rt = (RichTextBox)sender;
            string richText = new TextRange(rt.Document.ContentStart, rt.Document.ContentEnd).Text;
            Console.WriteLine(richText);
        }
        private void fontSizeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem value = (ComboBoxItem)comboBox.SelectedValue;
            TextRange tr = new TextRange(textBoxMain.Selection.Start, textBoxMain.Selection.End);
            tr.ApplyPropertyValue(TextElement.FontSizeProperty, value.Content);

            //textBoxMain.FontSize = double.Parse(value.Content.ToString());
            //textBoxMain.cha
        }


    }
}
