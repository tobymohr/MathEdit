using System;
using System.Windows.Documents;

namespace MathEdit.Model
{
    [Serializable]
    public class EnabledFlowDocument : FlowDocument
    {
        #region Fields
        public ListOfOperations childrenOperations { get; set; }
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
