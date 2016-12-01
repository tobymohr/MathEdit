using MathEdit.ModelHelpers;
using System;
using System.Linq;
using System.Windows;

namespace MathEdit.Model
{
    [Serializable]
    public class SquareModel : Operation
    {
        private double minWidth = 50;
        private Thickness t;

        public SquareModel()
        {
            t = new Thickness(1);
            OuterWidth = minWidth;
            Boxes = new ListOfEnabledDocs { new EnabledFlowDocument("")};
        }

        public double numberWidth { get { return getTotalWidth(Boxes.ElementAt(0)); } }

        public override double OuterWidth
        {
            get
            {
                double calcedWidth = getTotalWidth(Boxes.ElementAt(0)) + minWidth;
                return calcedWidth;
            }

            set
            {

                this.SetProperty(ref outerWidth, value);
            }
        }

        public Thickness numberborder
        {
            get
            {
                double length = Boxes.ElementAt(0).GetFormattedText().WidthIncludingTrailingWhitespace;
                if (length <= 0)
                {
                    t= new Thickness(1);
                }
                else
                {
                    t = new Thickness(0);
                }
                return t;
            }
            set
            {
                t = value;
            }
        }


        
    }
}
