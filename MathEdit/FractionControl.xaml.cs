using MathEdit.Models;
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
    /// <summary>
    /// Interaction logic for FractionControl.xaml
    /// </summary>
    public partial class FractionControl : UserControl
    {
        private double minWidth = 50;
        private double minParentWidth = 70;
        public FractionModel model { get; set; }
        
        public FractionControl()
        {
            model = new FractionModel();
            DataContext = model;
            InitializeComponent();
            TrackSurface.Width = minParentWidth;
            numenatorTextBox.Document = new EnabledFlowDocument();
            denumenatorTextBox.Document = new EnabledFlowDocument();
            numenatorTextBox.TextChanged += onChange;
            denumenatorTextBox.TextChanged += onChange;
        }

        public void onChange(object sender, RoutedEventArgs e)
        {
            RichTextBox tb = sender as RichTextBox;
            EnabledFlowDocument flowDock = tb.Document as EnabledFlowDocument;
            if (tb.Name != "FirstBox")
            {
                double newWidth = tb.Document.GetFormattedText().WidthIncludingTrailingWhitespace;
                tb.Width = newWidth;
                if(newWidth > TrackSurface.Width)
                {
                    TrackSurface.Width = newWidth;
                }
                foreach (IOperation op in flowDock.childrenOperations)
                {
                    foreach (EnabledFlowDocument tempDock in op.getChildren())
                    {

                    }
                }
            }
        }

        

        private void resizeParent(object sender)
        {
            RichTextBox rt = (RichTextBox)sender;
            DependencyObject parentObject = VisualTreeHelper.GetParent(rt);
            Grid parentGrid = parentObject as Grid;
            if (parentGrid != null)
            {
                parentGrid.Width = minWidth;
            }

            RichTextBox parent = findParent(rt);
            while (parent != null)
            {
                if (parent != null && parent != sender)
                {
                    if (parent.Name != "textBoxMain")
                    {
                        if(numenatorTextBox.Width <= minParentWidth && denumenatorTextBox.Width <= minParentWidth)
                        {
                            Console.WriteLine("Resizing to default");
                            parent.Width = minParentWidth + 15;
                            parentGrid.Width = minParentWidth;
                        }
                        else
                        {
                            parent.Width = minWidth + 20;
                        }
                       
                    }
                }
                parent = findParent(parent);
            }
        }

        private void resizeRichTextBox(RichTextBox rt)
        {
            if (rt.IsFocused)
            {
                double newWidth = rt.Document.GetFormattedText().WidthIncludingTrailingWhitespace;
                rt.Width = newWidth;
                if (newWidth > minWidth)
                {
                    minWidth = newWidth;
                }
                else if (newWidth <= 0)
                {
                    rt.Width = minParentWidth;
                }
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
    }

}
