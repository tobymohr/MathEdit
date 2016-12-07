using MathEdit.ModelHelpers;
using System;
using System.Linq;
using System.Windows;

namespace MathEdit.Model
{
    [Serializable]
    public class PowModel : Operation
    {
        private double _outerWidth;
        private ListOfEnabledDocs _boxes;
        private int minWidth = 50;
        private int margin =10;
        private int myPostion = 0;
        private EnabledFlowDocument parent;
        public PowModel(EnabledFlowDocument parent)
        {
            this.parent = parent;
            outerWidth = minWidth;
            ListOfEnabledDocs = new ListOfEnabledDocs { new EnabledFlowDocument(""), new EnabledFlowDocument("") };
        }

        public PowModel()
        {

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

        public override int position
        {
            get
            {
                return myPostion;
            }

            set
            {
                myPostion = value;
            }
        }
    }
}
