using System;
using System.Windows.Documents;

namespace MathEdit.Model
{
    [Serializable]
    public class EnabledFlowDocument : FlowDocument
    {
        #region Fields
        private ListOfOperations _childrenOperations;
        #endregion

        public EnabledFlowDocument(string id)
        {
            childrenOperations = new ListOfOperations();
        }
        public EnabledFlowDocument()
        {
         
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
