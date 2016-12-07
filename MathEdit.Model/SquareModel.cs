using MathEdit.ModelHelpers;
using System;
using System.Linq;
using System.Windows;

namespace MathEdit.Model
{
    [Serializable]
    public class SquareModel : Operation
    {
        private ListOfEnabledDocs _boxes;
        private double minWidth = 50;
        private double _outerWidth;
        private int myPostion = 0;
        public SquareModel()
        {
 
        }

        public SquareModel(string id)
        {
            outerWidth = minWidth;
            _boxes = new ListOfEnabledDocs { new EnabledFlowDocument("") };
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
