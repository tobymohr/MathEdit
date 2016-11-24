using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEdit.Models
{
    public class FractionModel : IOperation
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private List<EnabledFlowDocument> children;

        public FractionModel()
        {
            children = new List<EnabledFlowDocument>();
        }
        public List<EnabledFlowDocument> getChildren()
        {
            return children;
        }

        public double getWidth()
        {
            throw new NotImplementedException();
        }

        public void onIOperationChanged()
        {
            throw new NotImplementedException();
        }

        public double setWidth(double width)
        {
            throw new NotImplementedException();
        }
    }
}
