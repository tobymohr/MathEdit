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
    public class SquareModel : Operation
    {
        private ListOfEnabledDocs _boxes;
        private double minWidth = 50;
        private Thickness t;
        private double _outerWidth;

        public SquareModel()
        {
            t = new Thickness(1);
            outerWidth = minWidth;
            _boxes = new ListOfEnabledDocs { new EnabledFlowDocument("")};
        }

        public double numberWidth { get { return getTotalWidth(_boxes.ElementAt(0)); } }

        public override double outerWidth
        {
            get
            {
                double calcedWidth = getTotalWidth(_boxes.ElementAt(0)) + minWidth;
                return calcedWidth;
            }

            set
            {

                this._outerWidth = value;
            }
        }

        public Thickness numberborder
        {
            get
            {
                double length = _boxes.ElementAt(0).GetFormattedText().WidthIncludingTrailingWhitespace;
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
