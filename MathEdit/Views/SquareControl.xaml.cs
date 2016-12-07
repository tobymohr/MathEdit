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
        public SquareControl(EnabledFlowDocument parent)
        {
            model = new SquareModel(parent);
            DataContext = model;
            InitializeComponent();
            numberBox.Document = model.ListOfEnabledDocs.ElementAt(0);
            numberBox.TextChanged += onChange;
            setUIWidth();
            
        }

        public void onChange(object sender, RoutedEventArgs e)
        {
            setUIWidth();
        }

        private void setUIWidth()
        {
            numberBox.Width = model.numberWidth + 20;
            TrackSurface.Width = model.outerWidth + 20;
        }
    }
}
