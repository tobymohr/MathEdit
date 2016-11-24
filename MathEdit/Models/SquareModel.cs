using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MathEdit.Models
{
    [Serializable]
    public class SquareModel : IOperation
    {
        public List<EnabledFlowDocument> boxes;
        public double width;

        public SquareModel()
        {
            boxes = new List<EnabledFlowDocument>();
            boxes.Add(new EnabledFlowDocument());
        }
        List<EnabledFlowDocument> IOperation.boxes
        {
            get
            {
                return boxes;
            }
        }

        double IOperation.width
        {
            get
            {
                return width;
            }

            set
            {
                this.width = value;
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }

        public void onIOperationChanged()
        {
            throw new NotImplementedException();
        }
    }
}
