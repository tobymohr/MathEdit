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
        private double _outerWidth;
        private ListOfEnabledDocs _boxes;
        private EnabledFlowDocument parent;
        public FractionModel()
        {

        }

        public FractionModel(EnabledFlowDocument parent)
        {
            this.parent = parent;
            outerWidth = 70;
            ListOfEnabledDocs = new ListOfEnabledDocs { new EnabledFlowDocument(""), new EnabledFlowDocument("") };
        }

        public EnabledFlowDocument getParent()
        {
            return parent;
        }
        public Thickness numborder
        {
            get
            {
                double length = _boxes.ElementAt(0).GetFormattedText().WidthIncludingTrailingWhitespace;
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
                double length = _boxes.ElementAt(1).GetFormattedText().WidthIncludingTrailingWhitespace;
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

        public double numenatorWidth { get { return getTotalWidth(_boxes.ElementAt(0)) + 10; } }
        public double denumenatorWidth { get { return getTotalWidth(_boxes.ElementAt(1)) + 10; } }

        [XmlElement("outerWidth")]
        public override double outerWidth
        {
            get
            {
                double calcedWidth = Math.Max(getTotalWidth(_boxes.ElementAt(0)), getTotalWidth(_boxes.ElementAt(1)));
                calcedWidth = calcedWidth + 40;
                return calcedWidth;
            }

            set
            {
                this._outerWidth = value;
            }
        }
        public override ListOfEnabledDocs ListOfEnabledDocs
        {
            get
            {
                return _boxes;
            }

            set
            {
                _boxes = value;
            }
        }

    }
}