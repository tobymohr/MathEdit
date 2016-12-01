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

        private int minWidth = 50;
        private int margin =10;
        public PowModel()
        {
            Width = minWidth;
            Boxes = new ListOfDocs { new FlowDocument(), new FlowDocument() };
        }

        //public double powWidth { get { return getTotalWidth(Boxes.ElementAt(0)) + margin; } }
        //public double numberWidth { get { return getTotalWidth(Boxes.ElementAt(1)) + margin; } }

        public override double Width
        {
            get
            {
                double calcedWidth = Boxes.ElementAt(0).GetFormattedText().WidthIncludingTrailingWhitespace +
                    Boxes.ElementAt(1).GetFormattedText().WidthIncludingTrailingWhitespace;
                if(calcedWidth < minWidth)
                {
                    calcedWidth = minWidth;
                }
                return calcedWidth + margin;
            }

            set
            {
                this.SetProperty(ref width, value);
            }
        }

        public Thickness numborder
        {
            get
            {
                double length = Boxes.ElementAt(1).GetFormattedText().WidthIncludingTrailingWhitespace;
                if (length <= 0)
                {
                    return new Thickness(1);
                }
                else
                {
                    return new Thickness(0);
                }
            }
        }
        public Thickness powborder
        {
            get
            {
                double length = Boxes.ElementAt(0).GetFormattedText().WidthIncludingTrailingWhitespace;
                if (length <= 0)
                {
                    return new Thickness(1);
                }
                else
                {
                    return new Thickness(0);
                }
            }
        }

       
    }
}
