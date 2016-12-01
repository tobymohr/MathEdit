using MathEdit.Model;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MathEdit.Views
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
