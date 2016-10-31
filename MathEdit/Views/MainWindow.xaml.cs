using Microsoft.Win32;
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

using MathEdit.Views;
using MathEdit.Services;

namespace MathEdit
{
    public partial class MainWindow : Window
    {
        string filename = "";
        EnabledFlowDocument fd = new EnabledFlowDocument();
        //fix for fontSizeBox first run
        bool fr = true;

        public MainWindow()
        {
            InitializeComponent();
            textBoxMain.Document = fd;
        }


        private void MenuItem_Add_Click(object sender, RoutedEventArgs e)
        {
            createNewRtb();
        }

        private void createNewRtb()
        {
            Paragraph para = null;
            if (fd.Blocks.Count == 0)
            {
                para = new Paragraph();
                fd.Blocks.Add(para);
                string text = String.Join(String.Empty, para.Inlines.Select(line => line.ContentStart.GetTextInRun(LogicalDirection.Forward)));
                para.Inlines.Add(text);
            }
            else if (fd.Blocks.Count > 0)
            {
                para = (Paragraph)fd.Blocks.LastBlock;
            }
            RichTextBox rtb = new RichTextBox() { Focusable = true };

            rtb.Width = 100;
            rtb.Focusable = true;
            rtb.Focus();
            rtb.TextChanged += onTextChanged;

            SquareControl sqr = new SquareControl();
            EnabledFlowDocument rtbDoc = new EnabledFlowDocument();
            rtb.Document = rtbDoc;
            Paragraph rtbPara = new Paragraph();
            rtbDoc.Blocks.Add(rtbPara);
            rtbPara.Inlines.Add(sqr);
            para.Inlines.Add(rtb);

        }

        private void MouseEnter(Object sender, RoutedEventArgs e)
        {
            RichTextBox rt = (RichTextBox)sender;
            rt.Focus();
            Console.WriteLine("Mouse entered");
        }



        private void onTextChanged(object sender, EventArgs e)
        {
            RichTextBox rt = (RichTextBox)sender;
            //string richText = new TextRange(rt.Document.ContentStart, rt.Document.ContentEnd).Text;
        }
        private void fontSizeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ComboBox comboBox = (ComboBox)sender;
            string value = (string)comboBox.SelectedValue;
            if (comboBox.SelectedIndex > -1 && value != null && !fr)
            {
                TextSelection text = textBoxMain.Selection;
                textBoxMain.Focus();
                text.ApplyPropertyValue(RichTextBox.FontSizeProperty, value);
            }
            fr = false;
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void textBoxMain_SelectionChanged(object sender, RoutedEventArgs e)
        {
            //Changes combobox to
            TextSelection text = textBoxMain.Selection;
            object trsize = text.GetPropertyValue(TextElement.FontSizeProperty);
            int fs;
            if (Int32.TryParse(trsize.ToString(), out fs))
            {
                fontSizeBox.Text = trsize.ToString();
            }
            else
            {
                fontSizeBox.SelectedIndex = -1;
            }
            //RichTextBox rtb = (RichTextBox)sender;
            //TextPointer tp = rtb.GetPositionFromPoint()
        }

        private void OpenCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // needs work
            DocumentHelper helper = new DocumentHelper();
            fd = helper.openFile();
        }

        private void SaveCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // works
            DocumentHelper helper = new DocumentHelper();
            if (filename == "")
            {
                helper.saveDoc(fd);
            }
            else
            {
                helper.saveDoc(fd, filename);
            }
        }

        private void SaveAsCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DocumentHelper helper = new DocumentHelper();
            helper.saveDocAs(fd);
        }
    }
}
