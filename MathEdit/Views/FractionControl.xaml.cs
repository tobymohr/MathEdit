using MathEdit.Model;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MathEdit.Views
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
            numenatorTextBox.Document = model.ListOfEnabledDocs.ElementAt(0);
            denumenatorTextBox.Document = model.ListOfEnabledDocs.ElementAt(1);
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
