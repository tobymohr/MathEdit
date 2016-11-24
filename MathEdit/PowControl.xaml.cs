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
            InitializeComponent();
        }
    }
}
