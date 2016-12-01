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
        private double minWidth = 50;
        private Thickness t;

        public SquareModel()
        {
            t = new Thickness(1);
            Width = minWidth;
            Boxes = new ListOfDocs { new FlowDocument()};
        }

        //public double numberWidth { get { return getTotalWidth(Boxes.ElementAt(0)); } }

        public override double Width
        {
            get
            {
                double calcedWidth = Boxes.ElementAt(0).GetFormattedText().WidthIncludingTrailingWhitespace + minWidth;
                return calcedWidth;
            }

            set
            {

                this.SetProperty(ref width, value);
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
