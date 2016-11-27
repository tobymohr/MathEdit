using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MathEdit.Models
{
    [Serializable]
    public class FractionModel : Operation
    {
        public ListOfEnabledDocs _boxes;
        public double _outerWidth, _width;

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
            outerWidth = 30;
            _boxes = new ListOfEnabledDocs { new EnabledFlowDocument(), new EnabledFlowDocument() };
        }

        public override double width
        {
            get
            {
                return this._width;
            }

            set
            {
                this._width = value;
            }
        }

        public override double outerWidth
        {
            get
            {
                return this._outerWidth;
            }

            set
            {
                this._outerWidth = value;
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
