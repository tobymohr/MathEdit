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

namespace MathEdit.Models
{
    [Serializable]
    public class PowModel : Operation
    {
        private double _outerWidth;
        private ListOfEnabledDocs _boxes;
        private int minWidth = 50;
        private int margin =10;
        public PowModel()
        {
            outerWidth = minWidth;
            _boxes = new ListOfEnabledDocs { new EnabledFlowDocument(""), new EnabledFlowDocument("") };
        }

        public double powWidth { get { return getTotalWidth(_boxes.ElementAt(0)) + margin; } }
        public double numberWidth { get { return getTotalWidth(_boxes.ElementAt(1)) + margin; } }

        public override double outerWidth
        {
            get
            {
                double calcedWidth = getTotalWidth(_boxes.ElementAt(0)) +  getTotalWidth(_boxes.ElementAt(1));
                if(calcedWidth < minWidth)
                {
                    calcedWidth = minWidth;
                }
                return calcedWidth + margin;
            }

            set
            {
                this._outerWidth = value;
            }
        }

        public Thickness numborder
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
        public Thickness powborder
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
