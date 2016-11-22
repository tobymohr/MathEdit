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


        public PowControl()
        {
            InitializeComponent();
            pow.TextChanged += onTextChanged;
            number.TextChanged += onTextChanged;
        }

        private void onTextChanged(object sender, EventArgs e)
        {
            RichTextBox rt = (RichTextBox)sender;
            if (rt.IsFocused)
            {
                double newWidth = rt.Document.GetFormattedText().WidthIncludingTrailingWhitespace;
                rt.Width = newWidth;
                if(newWidth <= 30)
                {
                    if (rt.Name.Equals(pow.Name))
                    {
                        rt.Width = pow.MinWidth;
                    }else
                    {
                        rt.Width = number.MinWidth;
                    }
                }

            }
            DependencyObject parentObject = VisualTreeHelper.GetParent(rt);
            Grid parentGrid = parentObject as Grid;
            if (parentGrid != null)
            {
                parentGrid.Width = number.Width + pow.Width + 20;
            }

            RichTextBox parent = findParent(rt);
            while (parent != null)
            {
                if (parent != null && parent != sender)
                {
                    if (parent.Name != "textBoxMain")
                    {
                        parent.Width = number.Width + pow.Width + 20;
                        parent.Height = parentGrid.Height;
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
    }
}
