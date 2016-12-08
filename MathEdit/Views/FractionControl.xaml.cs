using MathEdit.Helpers;
using MathEdit.Model;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System;
using MathEdit.ViewModels;
using System.Windows.Documents;

namespace MathEdit.Views
{
    /// <summary>
    /// Interaction logic for FractionControl.xaml
    /// </summary>
    public partial class FractionControl : UserControl, MathControl
    {
        private Operation model;

        Operation MathControl.model
        {
            get
            {
                return model;
            }

            set
            {
                this.model = value;
            }
        }

        public FractionControl(EnabledFlowDocument parent)
        {
            model = new FractionModel(parent);
            InitializeComponent();
            numeratorTextBox.Document = model.ListOfEnabledDocs.ElementAt(0);
            denominatorTextBox.Document = model.ListOfEnabledDocs.ElementAt(1);
            numeratorTextBox.TextChanged += onChange;
            denominatorTextBox.TextChanged += onChange;
            setUIWidth();
        }

        public void onChange(object sender, RoutedEventArgs e)
        {
            setUIWidth();
        }

        public void setUIWidth()
        {
            FractionModel model = (FractionModel)this.model;
            denominatorTextBox.Width = model.denumenatorWidth + 15;
            numeratorTextBox.Width = model.numenatorWidth + 15;
            TrackSurface.Width = model.outerWidth;
        }
    }
}
