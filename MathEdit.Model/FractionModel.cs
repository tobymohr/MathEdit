using MathEdit.ModelHelpers;
using System;
using System.Linq;
using System.Windows;
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
            OuterWidth = 70;
            Boxes = new ListOfEnabledDocs { new EnabledFlowDocument(""), new EnabledFlowDocument("") };
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

        public double numenatorWidth { get { return getTotalWidth(Boxes.ElementAt(0)) + 10; } }
        public double denumenatorWidth { get { return getTotalWidth(Boxes.ElementAt(1)) + 10; } }

        [XmlElement("outerWidth")]
        public override double OuterWidth
        {
            get
            {
                double calcedWidth = Math.Max(getTotalWidth(Boxes.ElementAt(0)), getTotalWidth(Boxes.ElementAt(1)));
                calcedWidth = calcedWidth + 40;
                return calcedWidth;
            }

            set
            {
                this.SetProperty(ref outerWidth, value);
            }
        }

       


    }
}
