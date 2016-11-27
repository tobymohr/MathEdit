using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Xml;
using System.Xml.Serialization;

namespace MathEdit.Models
{
    [Serializable]
    public class EnabledFlowDocument : FlowDocument
    {
        #region Fields
        public ListOfOperations _childrenOperations;
        #endregion

        public EnabledFlowDocument()
        {
            childrenOperations = new ListOfOperations();
        }
         public ListOfOperations childrenOperations
        {
            get
            {
                return _childrenOperations;
            }
            set
            {
                _childrenOperations = value;
            }
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
