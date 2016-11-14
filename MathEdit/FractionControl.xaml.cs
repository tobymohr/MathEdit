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
        
        public FractionControl()
        {
            InitializeComponent();
            numenatorTextBox.TextChanged += onTextChanged;
            denumenatorTextBox.TextChanged += onTextChanged;
           
        }


        private void onTextChanged(object sender, EventArgs e)
        {
            RichTextBox rt = (RichTextBox)sender;
            if (rt.IsFocused)
            {
                double newWidth = rt.Document.GetFormattedText().WidthIncludingTrailingWhitespace;
                if(newWidth > minWidth)
                {
                    rt.Width = newWidth;
                    minWidth = newWidth;
                }else if(newWidth == 0)
                {
                    minWidth = 50;
                }
            }
            DependencyObject parentObject = VisualTreeHelper.GetParent(rt);
            Grid parentGrid = parentObject as Grid;
            if (parentGrid != null)
            {
                parentGrid.Width = minWidth;
            }
        }
    }
}
