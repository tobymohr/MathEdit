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
        public PowModel model { get; set; }
        public PowControl()
        {
            model = new PowModel();
            DataContext = model;
            InitializeComponent();
            pow.Document = model.boxes.ElementAt(0);
            number.Document = model.boxes.ElementAt(1);
            pow.TextChanged += onChange;
            number.TextChanged += onChange;
        }

        public void onChange(object sender, RoutedEventArgs e)
        {
            pow.Width = model.powWidth;
            number.Width = model.numberWidth;
            TrackSurface.Width = model.outerWidth;
            number.BorderThickness = model.numborder;
            pow.BorderThickness = model.powborder;
        }
    }
}
