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
using System.Globalization;
using System.Windows.Threading;
using System.Threading;
using MathEdit.Services;

namespace MathEdit
{
    public partial class MainWindow : Window
    {
        string filename = "";
        EnabledFlowDocument fd = new EnabledFlowDocument();
        

        public MainWindow()
        {
            InitializeComponent();
            
            textBoxMain.Document = fd;
        }

        private void OpenCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.DefaultExt = "xml";
            openDialog.Filter = "XML Files|*.xml";

            Nullable<bool> result = openDialog.ShowDialog();
            if (result == true)
            {
                string filename = openDialog.FileName;
            }
        }
        private void SaveCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // Need to find a way to parse content to an XML doc
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.FileName = "Sheet";
            saveDialog.DefaultExt = ".xml";
            saveDialog.Filter = "XML Files|*.xml";

            Nullable<bool> result = saveDialog.ShowDialog();
            if (result == true)
            {
                string filename = saveDialog.FileName;
            }
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
            
            rtb.Focusable = true;
            rtb.Focus();
            rtb.Width = 20;
            rtb.TextChanged += new TextChangedEventHandler((o, e) => rtb.Width = rtb.Document.GetFormattedText().WidthIncludingTrailingWhitespace + 20);

            //SquareControl sqr = new SquareControl();
            //RichTextBox sqrtRtb = sqr.getRichTextBox();
            //sqrtRtb.TextChanged += new TextChangedEventHandler((o, e) => sqrtRtb.Width = sqrtRtb.Document.GetFormattedText().WidthIncludingTrailingWhitespace + 20);


            //rtb.Document = rtbDoc;
            //
            //
            //rtbPara.Inlines.Add(sqr);
            Focus(rtb);
            para.Inlines.Add(rtb);

        }

        public  void Focus(UIElement element)
        {
            if (!element.Focus())
            {
                element.Dispatcher.BeginInvoke(DispatcherPriority.Input, new ThreadStart(delegate ()
                {
                    element.Focus();
                }));
            }
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
            if (comboBox.IsDropDownOpen)
            {
                TextSelection text = textBoxMain.Selection;
                textBoxMain.Focus();
                text.ApplyPropertyValue(RichTextBox.FontSizeProperty, value);
            }
            
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

    public static class FlowDocumentExtensions
    {
        private static IEnumerable<TextElement> GetRunsAndParagraphs(FlowDocument doc)
        {
            for (TextPointer position = doc.ContentStart;
              position != null && position.CompareTo(doc.ContentEnd) <= 0;
              position = position.GetNextContextPosition(LogicalDirection.Forward))
            {
                if (position.GetPointerContext(LogicalDirection.Forward) == TextPointerContext.ElementEnd)
                {
                    Run run = position.Parent as Run;

                    if (run != null)
                    {
                        yield return run;
                    }
                    else
                    {
                        Paragraph para = position.Parent as Paragraph;

                        if (para != null)
                        {
                            yield return para;
                        }
                    }
                }
            }
        }

        public static FormattedText GetFormattedText(this FlowDocument doc)
        {
            if (doc == null)
            {
                throw new ArgumentNullException("doc");
            }

            FormattedText output = new FormattedText(
              GetText(doc),
              CultureInfo.CurrentCulture,
              doc.FlowDirection,
              new Typeface(doc.FontFamily, doc.FontStyle, doc.FontWeight, doc.FontStretch),
              doc.FontSize,
              doc.Foreground);

            int offset = 0;

            foreach (TextElement el in GetRunsAndParagraphs(doc))
            {
                Run run = el as Run;

                if (run != null)
                {
                    int count = run.Text.Length;

                    output.SetFontFamily(run.FontFamily, offset, count);
                    output.SetFontStyle(run.FontStyle, offset, count);
                    output.SetFontWeight(run.FontWeight, offset, count);
                    output.SetFontSize(run.FontSize, offset, count);
                    output.SetForegroundBrush(run.Foreground, offset, count);
                    output.SetFontStretch(run.FontStretch, offset, count);
                    output.SetTextDecorations(run.TextDecorations, offset, count);

                    offset += count;
                }
                else
                {
                    offset += Environment.NewLine.Length;
                }
            }

            return output;
        }

        private static string GetText(FlowDocument doc)
        {
            StringBuilder sb = new StringBuilder();

            foreach (TextElement el in GetRunsAndParagraphs(doc))
            {
                Run run = el as Run;
                sb.Append(run == null ? Environment.NewLine : run.Text);
            }
            return sb.ToString();
        }
    }
}
