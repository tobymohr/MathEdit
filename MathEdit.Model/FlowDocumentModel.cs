namespace MathEdit.Model
{
    public class FlowDocumentModel
    {
        public FlowDocumentModel() { }

        public EnabledFlowDocument mainFlowDocument {get; set;}
        public byte[] binaryFlowDocument { get; set; } //TODO slet senere når toby har fikset livet
    }
}
