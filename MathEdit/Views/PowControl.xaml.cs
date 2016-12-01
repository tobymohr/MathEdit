using MathEdit.Model;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MathEdit.Views
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
