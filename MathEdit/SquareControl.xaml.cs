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
                TrackSurface.Width += model.width;
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
