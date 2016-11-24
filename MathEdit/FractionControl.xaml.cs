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
        private double minParentWidth = 70;
        private double prevWidth = 0;
        public FractionModel model { get; set; }

        public FractionControl()
        {
            model = new FractionModel();
            DataContext = model;
            InitializeComponent();
            TrackSurface.Width = minParentWidth;
            numenatorTextBox.Document = model.boxes.ElementAt(0);
            denumenatorTextBox.Document = model.boxes.ElementAt(1);
            numenatorTextBox.TextChanged += onChange;
            denumenatorTextBox.TextChanged += onChange;
        }

        public void onChange(object sender, RoutedEventArgs e)
        {
            RichTextBox tb = sender as RichTextBox;
            EnabledFlowDocument flowDoc = tb.Document as EnabledFlowDocument;
            if (tb.Name != "FirstBox")
            {
                model.width = getTotalWidth(flowDoc) ;
                tb.Width = model.width + 20;
                double outerWidth = Math.Max(getTotalWidth(model.boxes.ElementAt(0)), getTotalWidth(model.boxes.ElementAt(1)));
                Console.WriteLine(this.Name + " " +  outerWidth);
                TrackSurface.Width = outerWidth+40;
            }
        }

        private double getTotalWidth(EnabledFlowDocument model)
        {
            double maxValue = 0;
            double textWidth = model.GetFormattedText().WidthIncludingTrailingWhitespace;
            double sumWidth = 0;
            foreach (IOperation op in model.childrenOperations)
            {
                sumWidth += op.width;
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
