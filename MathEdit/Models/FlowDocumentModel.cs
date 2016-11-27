using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace MathEdit.Models
{
    class FlowDocumentModel
    {
        public FlowDocumentModel() { }

        public EnabledFlowDocument mainFlowDocument {get; set;}
        public byte[] binaryFlowDocument { get; set; }
    }
}
