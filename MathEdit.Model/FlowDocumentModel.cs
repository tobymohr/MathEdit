namespace MathEdit.Model
{
    public class FlowDocumentModel
    {
        private EnabledFlowDocument enabledFlowDocument;
        public FlowDocumentModel() {
            mainFlowDocument = new EnabledFlowDocument("");
        }

        public EnabledFlowDocument mainFlowDocument { get;set;}
        public byte[] binaryFlowDocument { get; set; } //TODO slet senere når toby har fikset livet
    }
}
