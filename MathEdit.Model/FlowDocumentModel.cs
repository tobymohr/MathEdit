namespace MathEdit.Model
{
    public class FlowDocumentModel
    {
        public FlowDocumentModel() {
            mainFlowDocument = new EnabledFlowDocument("");
        }

        public EnabledFlowDocument mainFlowDocument { get;set; } // Extracted from the main richtextbox.document
        public byte[] flowDocumentBytes { get; set; } // mainFlowDocument ASCII conversion for saving.
    }
}
