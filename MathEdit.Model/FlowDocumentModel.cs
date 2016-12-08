namespace MathEdit.Model
{
    public class FlowDocumentModel
    {
        public FlowDocumentModel() {
            mainFlowDocument = new EnabledFlowDocument("");
        }

        public EnabledFlowDocument mainFlowDocument { get;set;}
        public byte[] flowDocumentBytes { get; set; }
    }
}
