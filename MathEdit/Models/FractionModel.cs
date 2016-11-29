using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace MathEdit.Models
{
    [Serializable]
    public class FractionModel : Operation
    {
        private double _outerWidth;
        private  ListOfEnabledDocs _boxes;
        public FractionModel()
        {
            
        }

        public FractionModel(string id)
        {
            outerWidth = 70;
            boxes = new ListOfEnabledDocs { new EnabledFlowDocument(""), new EnabledFlowDocument("") };
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
        public override ListOfEnabledDocs boxes
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
