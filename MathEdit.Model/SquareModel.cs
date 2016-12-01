using MathEdit.ModelHelpers;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Documents;

namespace MathEdit.Model
{
    [Serializable]
    public class SquareModel : Operation
    {
        private FlowDocument number;

        public SquareModel()
        {

        }

        public SquareModel(string id)
        {
            Width = 70;
            Number = new FlowDocument();
        }

     

        public FlowDocument Number
        {
            get { return number; }
            set { this.SetProperty(ref number, value); }
        }


        //        public double numenatorWidth { get { return getTotalWidth(Boxes.ElementAt(0)) + 10; } }
        //public double denumenatorWidth { get { return getTotalWidth(Boxes.ElementAt(1)) + 10; } }

        public override double Width
        {
            get
            {
                double calcedWidth = Number.GetFormattedText().WidthIncludingTrailingWhitespace + 30;
                return calcedWidth;
            }

            set
            {
                this.SetProperty(ref width, value);
            }
        }



    }
}
