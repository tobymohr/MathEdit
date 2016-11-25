using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MathEdit.Models
{
    [Serializable]
    public class SquareModel : Operation
    {
        public List<EnabledFlowDocument> _boxes;
        public double _outerWidth, _width;

        public SquareModel()
        {
            boxes = new List<EnabledFlowDocument>();
            boxes.Add(new EnabledFlowDocument());
            boxes.Add(new EnabledFlowDocument());
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

        public override List<EnabledFlowDocument> boxes
        {
            get
            {
                return this._boxes;
            }
            set
            {
                this._boxes = value;
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
    }
}
