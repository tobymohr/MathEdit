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
    /// Interaction logic for PowControl.xaml
    /// </summary>
    public partial class PowControl : UserControl
    {
        private double minParentWidth = 70;
        private double prevWidth = 0;
        public PowModel model { get; set; }
        public PowControl()
        {
            model = new PowModel();
            model._outerWidth = 40;
            InitializeComponent();
            TrackSurface.Width = minParentWidth;
            pow.Document = model.boxes.ElementAt(0);
            number.Document = model.boxes.ElementAt(1);
            pow.TextChanged += onChange;
            number.TextChanged += onChange;
        }


        public void onChange(object sender, RoutedEventArgs e)
        {
            RichTextBox tb = sender as RichTextBox;
            EnabledFlowDocument flowDoc = tb.Document as EnabledFlowDocument;
            if (tb.Name != "FirstBox")
            {
                model.width = getTotalWidth(flowDoc);
                tb.Width = model.width + 20;
                double outerWidth = getTotalWidth(model.boxes.ElementAt(0)) + getTotalWidth(model.boxes.ElementAt(1));
                Console.WriteLine(this.Name + " " + outerWidth);
                model.outerWidth = outerWidth + 40;
                TrackSurface.Width = model.outerWidth;
                if (model.width > 0)
                {
                    tb.BorderThickness = new Thickness(0);
                }
                else
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
