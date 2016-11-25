using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MathEdit.Models
{
    [Serializable]
    public class PowModel : IOperation
    {
        public List<EnabledFlowDocument> boxes;
        public double width;

        public PowModel()
        {
            boxes = new List<EnabledFlowDocument>();
            boxes.Add(new EnabledFlowDocument());
            boxes.Add(new EnabledFlowDocument());
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

        List<EnabledFlowDocument> IOperation.boxes
        {
            get
            {
                return boxes;
            }
        }

        public int id
        {
            get
            {
                return id;
            }

            set
            {
                this.id = value;
            }
        }

        public double outerWidth
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }
    }
}
