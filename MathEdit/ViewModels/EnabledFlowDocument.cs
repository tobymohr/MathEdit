using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace MathEdit.Views
{
    class EnabledFlowDocument : FlowDocument
    {
        protected override bool IsEnabledCore
        {
            get
            {
                return true;
            }
        }
    }
}
