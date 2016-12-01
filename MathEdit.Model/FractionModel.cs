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
        
        public FractionModel()
        {
            
        }

        public FractionModel(string id)
        {
            Width = 70;
            Boxes = new ListOfDocs { new FlowDocument(), new FlowDocument() };
        }
        public Thickness numborder
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
        public Thickness denumborder
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

//        public double numenatorWidth { get { return getTotalWidth(Boxes.ElementAt(0)) + 10; } }
        //public double denumenatorWidth { get { return getTotalWidth(Boxes.ElementAt(1)) + 10; } }
        
        public override double Width
        {
            get
            {
                double calcedWidth = Math.Max(Boxes.ElementAt(0).GetFormattedText().WidthIncludingTrailingWhitespace,
                    Boxes.ElementAt(1).GetFormattedText().WidthIncludingTrailingWhitespace);
                return calcedWidth;
            }

            set
            {
                this.SetProperty(ref width, value);
            }
        }

       


    }
}
