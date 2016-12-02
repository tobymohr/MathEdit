using MathEdit.ModelHelpers;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Xml.Serialization;

namespace MathEdit.Model
{
    [Serializable]
    public class SquareModel : Operation
    {
        private string number;

        public SquareModel()
        {
            Width = 70;
        }


        public string Number
        {
            get { return number; }
            set { this.SetProperty(ref number, value); }
        }

        public override double Width
        {
            get
            {
                double calcedWidth = 40;
                return calcedWidth;
            }

            set
            {
                this.SetProperty(ref width, value);
            }
        }

    }
}
