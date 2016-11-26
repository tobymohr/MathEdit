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
    /// Interaction logic for SquareControl.xaml
    /// </summary>
    public partial class SquareControl : UserControl
    {
        public SquareModel model { get; set; }
        public SquareControl()
        {
            model = new SquareModel();
            InitializeComponent();
            model._outerWidth = 40;
            numberBox.Document = model.boxes.ElementAt(0);
            numberBox.TextChanged += onChange;
        }

        public void onChange(object sender, RoutedEventArgs e)
        {
            RichTextBox tb = sender as RichTextBox;
            EnabledFlowDocument flowDoc = tb.Document as EnabledFlowDocument;
            if (tb.Name != "FirstBox")
            {
                model.width = getTotalWidth(flowDoc);
                tb.Width = model.width + 20;
                double outerWidth = model.width + TrackSurface.MinWidth;
                model.outerWidth = outerWidth;
                TrackSurface.Width = model.outerWidth;
                if (model.width > 0) {
                    tb.BorderThickness = new Thickness(0);
                }else
                {
                    tb.BorderThickness = new Thickness(1);
                }
            }
        }

        private double getTotalWidth(EnabledFlowDocument model)
        {
            double maxValue = 0;
            double textWidth = model.GetFormattedText().WidthIncludingTrailingWhitespace;
            double sumWidth = 0;
            foreach (Operation op in model.childrenOperations)
            {
                sumWidth += op.outerWidth;
            }

            if (sumWidth > textWidth)
            {
                maxValue = sumWidth;
            }
            else
            {
                maxValue = textWidth;
            }

            return maxValue;
        }
    }
}
