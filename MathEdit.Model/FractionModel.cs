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
        private EnabledFlowDocument _parent;
        private int _parPostion = 0;
        private int _blockPosition = 0;

        public FractionModel()
        {

        }

        public FractionModel(EnabledFlowDocument parent)
        {
            _parent = parent;
            outerWidth = 70;
            ListOfEnabledDocs = new ListOfEnabledDocs { new EnabledFlowDocument(""), new EnabledFlowDocument("") };
        }


        public override EnabledFlowDocument getParent
        {
            get
            {
                return _parent;
            }
        }


        public double numenatorWidth { get { return getTotalWidth(ListOfEnabledDocs.ElementAt(0)); } }
        public double denumenatorWidth { get { return getTotalWidth(ListOfEnabledDocs.ElementAt(1)); } }

        [XmlElement("outerWidth")]
        public override double outerWidth
        {
            get
            {
                double calcedWidth = Math.Max(getTotalWidth(ListOfEnabledDocs.ElementAt(0)), getTotalWidth(ListOfEnabledDocs.ElementAt(1)));
                calcedWidth = calcedWidth + 40;
                return calcedWidth;
            }

            set
            {
                _outerWidth = value;
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