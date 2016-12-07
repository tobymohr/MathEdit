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
        private int myPostion = 0;
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
       

        public double numenatorWidth { get { return getTotalWidth(ListOfEnabledDocs.ElementAt(0)) + 10; } }
        public double denumenatorWidth { get { return getTotalWidth(ListOfEnabledDocs.ElementAt(1)) + 10; } }

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