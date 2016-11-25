using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MathEdit.Models
{
    [Serializable]
    public class FractionModel : IOperation
    {
        public List<EnabledFlowDocument> boxes;
        public double width;
        public double outerWidth;
        public int id;

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

        int IOperation.id
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

        double IOperation.outerWidth
        {
            get
            {
                return outerWidth;
            }

            set
            {
                this.outerWidth = value;
            }
        }

        public FractionModel()
        {
            boxes = new List<EnabledFlowDocument>();
            boxes.Add(new EnabledFlowDocument());
            boxes.Add(new EnabledFlowDocument());
        }


       
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }
    }
}
