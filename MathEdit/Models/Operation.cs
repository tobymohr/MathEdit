using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Xml;
using System.Xml.Serialization;

namespace MathEdit.Models
{
    [Serializable]
    public abstract class Operation :  INotifyPropertyChanged
    {
        abstract public double outerWidth { get; set; }
        abstract public event PropertyChangedEventHandler PropertyChanged;
        public ListOfEnabledDocs _boxes;
        public ListOfEnabledDocs boxes
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
        public virtual double getTotalWidth(EnabledFlowDocument model)
        {
            double maxValue = 0;
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
