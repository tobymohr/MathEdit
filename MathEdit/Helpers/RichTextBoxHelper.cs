using System.IO;
using MathEdit.Model;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;

namespace MathEdit.Helpers
{
    public class RichTextBoxHelper : DependencyObject
    {
        public static FlowDocument GetDocumentXaml(DependencyObject obj)
        {
            return (FlowDocument)obj.GetValue(DocumentXamlProperty);
        }
        public static void SetDocumentXaml(DependencyObject obj, FlowDocument value)
        {
            obj.SetValue(DocumentXamlProperty, value);
        }

        public static void AddBlock(FlowDocument from, FlowDocument to)
        {
            if (from != null)
            {
                TextRange range = new TextRange(from.ContentStart, from.ContentEnd);

                MemoryStream stream = new MemoryStream();

                System.Windows.Markup.XamlWriter.Save(range, stream);

                range.Save(stream, DataFormats.XamlPackage);

                TextRange textRange2 = new TextRange(to.ContentEnd, to.ContentEnd);

                textRange2.Load(stream, DataFormats.XamlPackage);
            }
        }

        public static readonly DependencyProperty DocumentXamlProperty =
            DependencyProperty.RegisterAttached(
              "DocumentXaml",
              typeof(FlowDocument),
              typeof(RichTextBoxHelper),
              new FrameworkPropertyMetadata
              {
                  BindsTwoWayByDefault = true,
                  PropertyChangedCallback = (obj, e) =>
                  {
                      var richTextBox = (RichTextBox)obj;
                      var oldFlow = richTextBox.Document;
                      FlowDocument newDoc = new FlowDocument();
                      AddBlock(oldFlow, newDoc);
                      string flowDocument = XamlWriter.Save(oldFlow);
                      newDoc = XamlReader.Load(new System.IO.MemoryStream(Encoding.Default.GetBytes(flowDocument))) as FlowDocument;
                      richTextBox.Document = newDoc;
                      // Set the document

                  }
              });
    }

}