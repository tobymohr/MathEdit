using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace MathEdit.Models
{
    [Serializable]

    public class EnabledFlowDocument : FlowDocument
    {
        #region Fields
        public List<Operation> childrenOperations { get; set; }
        #endregion

        public EnabledFlowDocument()
        {
            childrenOperations = new List<Operation>();
        }

        #region public methods
        #endregion


        // Enabled functionality for nested flowdocuments, paragraphs etc.
        protected override bool IsEnabledCore
        {
            get
            {
                return true;
            }
        }


    }
}
