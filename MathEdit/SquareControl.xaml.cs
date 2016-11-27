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
            DataContext = model;
            InitializeComponent();
            numberBox.Document = model.boxes.ElementAt(0);
            numberBox.TextChanged += onChange;
        }

        public void onChange(object sender, RoutedEventArgs e)
        {
            numberBox.Width = model.numberWidth + 20;
            numberBox.BorderThickness = model.numberborder;
            TrackSurface.Width = model.outerWidth;
        }

    }
}
