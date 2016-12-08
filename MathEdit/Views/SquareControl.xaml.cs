using MathEdit.Model;
using MathEdit.ViewModels;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System;

namespace MathEdit.Views
{
    /// <summary>
    /// Interaction logic for SquareControl.xaml
    /// </summary>
    public partial class SquareControl : UserControl, MathControl
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
                model = value;
            }
        }

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
            SquareModel sM = (SquareModel)model;
            numberBox.Width = sM.numberWidth + 20;
            TrackSurface.Width = sM.outerWidth + 20;
        }
    }
}
