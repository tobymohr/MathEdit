using MathEdit.ModelHelpers;
using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;

namespace MathEdit.Model
{
    [Serializable]
    public class FractionModel : Operation
    {
        [XmlIgnore]
        public ICommand ChangeWidth { get; set; }
        [XmlIgnore]
        public ICommand MouseDown { get; set; }
        private string numerator, denominator;
        
        public FractionModel()
        {
            Width = 70;
            ChangeWidth = new RelayCommand<object>(this.changeWidth);
            MouseDown = new RelayCommand<object>(mouseDown);
        }
       

        public string ContentTop
        {
            get
            {
                return numerator;
            }

            set
            {
                this.SetProperty(ref numerator, value);
            }
        }

        public string ContentBottom
        {
            get
            {
                return denominator;
            }

            set
            {
                this.SetProperty(ref denominator, value);
            }
        }

        public override double Width
        {
            get
            {
                return width;
            }

            set
            {
                this.SetProperty(ref width, value);
            }
        }

        private void changeWidth(object sender)
        {
            Console.WriteLine("luluulul" );
        }
    }
}
