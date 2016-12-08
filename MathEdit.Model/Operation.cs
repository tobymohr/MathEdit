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
        abstract public double outerWidth { get; set; }
        abstract public ListOfEnabledDocs ListOfEnabledDocs { get; set; }
        abstract public int parPosition { get; set; }
        abstract public int blockPosition { get; set; }

        public virtual double getTotalWidth(EnabledFlowDocument model)
        {
            double maxValue = 0;
            model.text = model.GetFormattedText().Text;
            double textWidth = model.GetFormattedText().WidthIncludingTrailingWhitespace;
            double sumWidth = 0;
            foreach (Operation op in model.childrenOperations)
            {
                sumWidth += op.outerWidth;
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
