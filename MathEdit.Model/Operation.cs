using System;
using System.Xml.Serialization;
using GalaSoft.MvvmLight;
using MathEdit.ModelHelpers;

namespace MathEdit.Model
{
    [Serializable]
    public abstract class Operation : NotifyBase
    {
        [XmlElement("outerWidth")]
        public double outerWidth;
        public int placement;
        public EnabledFlowDocument parent;
        public ListOfEnabledDocs boxes;

        abstract public double OuterWidth { get; set; }
        public int Placement { get { return placement; } set { this.SetProperty(ref placement, value); } }
        public EnabledFlowDocument Parent { get { return parent; } set { this.SetProperty(ref parent, value); } }
        public ListOfEnabledDocs Boxes { get { return boxes; } set { this.SetProperty(ref boxes, value); } }

        protected double getTotalWidth(EnabledFlowDocument model)
        {
            double maxValue = 0;
            double textWidth = model.GetFormattedText().WidthIncludingTrailingWhitespace;
            double sumWidth = 0;
            foreach (Operation op in model.childrenOperations)
            {
                sumWidth += op.OuterWidth;
            }

            if (sumWidth > textWidth)
            {
                maxValue = sumWidth;
            }
            else
            {
                maxValue = textWidth;
            }

            return maxValue;
        }

    }

    
}
