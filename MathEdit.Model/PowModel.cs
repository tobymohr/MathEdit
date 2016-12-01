using MathEdit.ModelHelpers;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Documents;

namespace MathEdit.Model
{
    [Serializable]
    public class PowModel : Operation
    {

        private FlowDocument number;
        private FlowDocument power;

        public PowModel()
        {

        }

        public PowModel(string id)
        {
            Width = 70;
            Number = new FlowDocument();
            Power = new FlowDocument();
        }

        public FlowDocument Number
        {
            get { return number; }
            set { this.SetProperty(ref number, value); }
        }

        public FlowDocument Power
        {
            get { return power; }
            set { this.SetProperty(ref power, value); }
        }


        //        public double numenatorWidth { get { return getTotalWidth(Boxes.ElementAt(0)) + 10; } }
        //public double denumenatorWidth { get { return getTotalWidth(Boxes.ElementAt(1)) + 10; } }

        public override double Width
        {
            get
            {
                double calcedWidth = Number.GetFormattedText().WidthIncludingTrailingWhitespace + Power.GetFormattedText().WidthIncludingTrailingWhitespace + 30;
                return calcedWidth;
            }

            set
            {
                this.SetProperty(ref width, value);
            }
        }


    }
}
