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
        private EnabledFlowDocument _parent;
        private int _parPostion = 0;
        private int _blockPosition = 0;
        public SquareModel()
        {
 
        }

        public SquareModel(EnabledFlowDocument parent)
        {
            _parent = parent;
            outerWidth = minWidth;
            _boxes = new ListOfEnabledDocs { new EnabledFlowDocument("") };
        }

       

        public override EnabledFlowDocument getParent
        {
            get
            {
                return _parent;
            }
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



        public override int parPosition
        {
            get
            {
                return _parPostion;
            }

            set
            {
                _parPostion = value;
            }
        }

        public override int blockPosition
        {
            get
            {
                return _blockPosition;
            }

            set
            {
                _blockPosition = value;
            }
        }

      
    }
}
