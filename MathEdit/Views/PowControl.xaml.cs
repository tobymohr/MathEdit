using MathEdit.Helpers;
using MathEdit.Model;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System;
using MathEdit.ViewModels;

namespace MathEdit.Views
{
    /// <summary>
    /// Interaction logic for PowControl.xaml
    /// </summary>
    public partial class PowControl : UserControl, MathControl
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

        public PowControl(EnabledFlowDocument parent)
        {
            model = new PowModel(parent);
            DataContext = model;
            InitializeComponent();
            pow.Document = model.ListOfEnabledDocs.ElementAt(0);
            number.Document = model.ListOfEnabledDocs.ElementAt(1);
            pow.TextChanged += onChange;
            number.TextChanged += onChange;
            setUIWidth();
        }

        public void onChange(object sender, RoutedEventArgs e)
        {
            setUIWidth();
        }

        public void setUIWidth()
        {
            PowModel pM = (PowModel)model;
            pow.Width = pM.powWidth;
            number.Width = pM.numberWidth;
            TrackSurface.Width = pM.outerWidth;
        }
    }
}
