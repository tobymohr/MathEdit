using MathEdit.ModelHelpers;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Xml.Serialization;

namespace MathEdit.Model
{
    [Serializable]
    public class FractionModel : Operation
    {
        private FlowDocument numerator;
        private FlowDocument denominator;
        
        public FractionModel()
        {
            
        }

        public FractionModel(string id)
        {
            Width = 70;
            Numerator = new FlowDocument();
            Denominator = new FlowDocument();
        }

        public FlowDocument Numerator
        {
            get { return numerator; }
            set { this.SetProperty(ref numerator, value); }
        }

        public FlowDocument Denominator
        {
            get { return denominator; }
            set { this.SetProperty(ref denominator, value); }
        }


//        public double numenatorWidth { get { return getTotalWidth(Boxes.ElementAt(0)) + 10; } }
        //public double denumenatorWidth { get { return getTotalWidth(Boxes.ElementAt(1)) + 10; } }
        
        public override double Width
        {
            get
            {
                double calcedWidth = Math.Max(Numerator.GetFormattedText().WidthIncludingTrailingWhitespace,
                    Denominator.GetFormattedText().WidthIncludingTrailingWhitespace) + 30;
                return calcedWidth;
            }

            set
            {
                this.SetProperty(ref width, value);
            }
        }

       


    }
}
