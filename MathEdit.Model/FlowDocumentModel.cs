namespace MathEdit.Model
{
    class FlowDocumentModel
    {
        public FlowDocumentModel() { }

        public EnabledFlowDocument mainFlowDocument {get; set;}
        public byte[] binaryFlowDocument { get; set; }
    }
}
