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
        public FractionModel model { get; set; }

        public FractionControl()
        {
            model = new FractionModel("hello");
            DataContext = model;
            InitializeComponent();
            numenatorTextBox.Document = model.boxes.ElementAt(0);
            denumenatorTextBox.Document = model.boxes.ElementAt(1);
            numenatorTextBox.TextChanged += onChange;
            denumenatorTextBox.TextChanged += onChange;
        }

        public void onChange(object sender, RoutedEventArgs e)
        {
            denumenatorTextBox.Width = model.denumenatorWidth;
            numenatorTextBox.Width = model.numenatorWidth;
            numenatorTextBox.BorderThickness = model.numborder;
            denumenatorTextBox.BorderThickness = model.denumborder;
            TrackSurface.Width = model.outerWidth;
        }
    }
}
