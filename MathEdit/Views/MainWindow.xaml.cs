﻿using Microsoft.Win32;
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
        EnabledFlowDocument fd = new EnabledFlowDocument();
        RichTextBox parentTb;
        int count = 0;
        private double minWidth = 0;

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
            IInputElement focusedControl = FocusManager.GetFocusedElement(this);
            parentTb = (RichTextBox)focusedControl;
            FlowDocument parentFd;

            if (parentTb.Document.GetType() == typeof(EnabledFlowDocument))
            {
                parentFd = parentTb.Document;
            }
            else
            {
                parentFd = new EnabledFlowDocument();
                parentTb.Document = parentFd;
            }

            if (parentFd.Blocks.Count == 0)
            {
                para = new Paragraph();
                parentFd.Blocks.Add(para);
                string text = String.Join(String.Empty, para.Inlines.Select(line => line.ContentStart.GetTextInRun(LogicalDirection.Forward)));
                para.Inlines.Add(text);
            }
            else if (parentFd.Blocks.Count > 0)
            {
                para = (Paragraph)parentFd.Blocks.LastBlock;
            }

            RichTextBox rtb = new RichTextBox() { Focusable = true };
            rtb.Focusable = true;
            rtb.Focus();
            count++;
            rtb.Name = "xx" + count + "xxx";
            rtb.TextChanged += onTextChanged;
            rtb.Document = new EnabledFlowDocument();
            Paragraph insideParagraph = new Paragraph();
           
            rtb.BorderThickness = new Thickness(0);
            Focus(rtb);
            FractionControl fControl = new FractionControl();
            SquareControl sControl = new SquareControl();
            rtb.Width = sControl.Width;
            insideParagraph.Inlines.Add(sControl);
            rtb.Document.Blocks.Add(insideParagraph);
            para.Inlines.Add(rtb);

        }

        public void Focus(UIElement element)
        {
            if (!element.Focus())
            {
                element.Dispatcher.BeginInvoke(DispatcherPriority.Input, new ThreadStart(delegate ()
                {
                    element.Focus();
                }));
            }
        }

        private void onTextChanged(object sender, EventArgs e)
        {
            RichTextBox rt = (RichTextBox)sender;
            if (rt.IsFocused)
            {
                double newWidth = rt.Document.GetFormattedText().WidthIncludingTrailingWhitespace ;
                if (newWidth > minWidth)
                {
                    minWidth = newWidth;
                    rt.Width = minWidth;
                }
            }

            RichTextBox parent = findParent(rt);
            while (parent != null)
            {
                if (parent != null && parent != sender)
                {
                    if (parent.Name != "textBoxMain")
                    {
                        parent.Width = minWidth + 20;
                    }
                }
                parent = findParent(parent);
            }

        }

        private RichTextBox findParent(DependencyObject sender)
        {
            if (sender != null)
            {
                DependencyObject parentObject = VisualTreeHelper.GetParent(sender);

                RichTextBox parentBox = parentObject as RichTextBox;
                if (parentBox != null)
                {
                    return parentBox;
                }
                return findParent(parentObject);
            }
            return null;

        }
        private void fontSizeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ComboBox comboBox = (ComboBox)sender;
            string value = (string)comboBox.SelectedValue;
            if (comboBox.IsDropDownOpen)
            {
                parentTb = (RichTextBox)FocusManager.GetFocusedElement(this);
                TextSelection text = parentTb.Selection;
                parentTb.Focus();
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
            parentTb = (RichTextBox)FocusManager.GetFocusedElement(this);
            TextSelection text = parentTb.Selection;
            object trsize = text.GetPropertyValue(RichTextBox.FontSizeProperty);
            int fs;
            if (Int32.TryParse(trsize.ToString(), out fs))
            {
                fontSizeBox.Text = trsize.ToString();
            }
            else
            {
                fontSizeBox.SelectedIndex = -1;
            }
            //Changes Italic checked
            try
            {
                FontStyle italic = (FontStyle)text.GetPropertyValue(RichTextBox.FontStyleProperty);
                if (italic == FontStyles.Normal)
                {
                    Italic.IsChecked = false;
                }
                else
                {
                    Italic.IsChecked = true;
                }
            }
            catch (Exception)
            {
                Italic.IsChecked = false;
            }
            //Changes Bold checked
            //Changes Italic checked
            try
            {
                FontWeight bold = (FontWeight)text.GetPropertyValue(RichTextBox.FontWeightProperty);
                if (bold == FontWeights.Normal)
                {
                    Bold.IsChecked = false;
                }
                else
                {
                    Bold.IsChecked = true;
                }
            }
            catch (Exception)
            {
                Bold.IsChecked = false;
            }
            //RichTextBox rtb = (RichTextBox)sender;
            //TextPointer tp = rtb.GetPositionFromPoint()
        }

        private void Bold_Click(object sender, RoutedEventArgs e)
        {
            if (Bold.IsChecked != null)
            {
                parentTb = (RichTextBox)FocusManager.GetFocusedElement(this);
                TextSelection text = parentTb.Selection;
                parentTb.Focus();
                if ((bool)Bold.IsChecked)
                {
                    text.ApplyPropertyValue(RichTextBox.FontWeightProperty, FontWeights.UltraBold);
                }
                else
                {
                    text.ApplyPropertyValue(RichTextBox.FontWeightProperty, FontWeights.Normal);
                }
            }
        }

        private void Italic_Click(object sender, RoutedEventArgs e)
        {
            if (Italic.IsChecked != null)
            {
                parentTb = (RichTextBox)FocusManager.GetFocusedElement(this);
                TextSelection text = parentTb.Selection;
                parentTb.Focus();
                if ((bool)Italic.IsChecked)
                {
                    text.ApplyPropertyValue(RichTextBox.FontStyleProperty, FontStyles.Oblique);
                }
                else
                {
                    text.ApplyPropertyValue(RichTextBox.FontStyleProperty, FontStyles.Normal);
                }
            }
        }

        private void menuItemFraction_Click(object sender, RoutedEventArgs e)
        {

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
