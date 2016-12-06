using MathEdit.Helpers;
using MathEdit.Model;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System;

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
            model = new FractionModel("");
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
            TrackSurface.Width = model.outerWidth;
        }
    }
}
