using System;
using System.Windows.Documents;

namespace MathEdit.Model
{
    [Serializable]
    public class EnabledFlowDocument : FlowDocument
    {
        #region Fields
        private ListOfOperations _childrenOperations;
        public string text { get; set; }
        #endregion



        #region public methods
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

        public void RemoveChild(Operation operation)
        {
            
        }
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
