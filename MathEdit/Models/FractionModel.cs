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
using System.Xml.Serialization;

namespace MathEdit.Models
{
    [Serializable]
    public class FractionModel : Operation
    {
        public double _outerWidth;
        public override event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            var propertyName = (propertyExpression?.Body as MemberExpression)?.Member?.Name;
            NotifyPropertyChanged(propertyName);
        }

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName != null && PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public FractionModel()
        {
            outerWidth = 70;
            _boxes = new ListOfEnabledDocs { new EnabledFlowDocument(), new EnabledFlowDocument() };
        }

        public Thickness numborder
        {
            get
            {
                double length = boxes.ElementAt(0).GetFormattedText().WidthIncludingTrailingWhitespace;
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
        public Thickness denumborder
        {
            get
            {
                double length = boxes.ElementAt(1).GetFormattedText().WidthIncludingTrailingWhitespace;
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

        public double numenatorWidth { get { return getTotalWidth(boxes.ElementAt(0)) + 10; } }
        public double denumenatorWidth { get { return getTotalWidth(boxes.ElementAt(1)) + 10; } }

        public override double outerWidth
        {
            get
            {
                double calcedWidth = Math.Max(getTotalWidth(boxes.ElementAt(0)), getTotalWidth(boxes.ElementAt(1)));
                calcedWidth = calcedWidth + 40;
                return calcedWidth;
            }

            set
            {
                this._outerWidth = value;
            }
        }

    }
}
