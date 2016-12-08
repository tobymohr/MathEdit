using MathEdit.Model;
using System.Windows;
using System.Windows.Controls;

namespace MathEdit.Helpers
{
    // Proxy class for binding RTB.document
    public class RichTextBoxHelper : DependencyObject
    {
        public static EnabledFlowDocument GetDocumentXaml(DependencyObject obj)
        {
            return (EnabledFlowDocument)obj.GetValue(DocumentXamlProperty);
        }
        public static void SetDocumentXaml(DependencyObject obj, EnabledFlowDocument value)
        {
            obj.SetValue(DocumentXamlProperty, value);
        }

        public static readonly DependencyProperty DocumentXamlProperty =
            DependencyProperty.RegisterAttached(
              "DocumentXaml",
              typeof(EnabledFlowDocument),
              typeof(RichTextBoxHelper),
              new FrameworkPropertyMetadata
              {
                  BindsTwoWayByDefault = true,
                  PropertyChangedCallback = (obj, e) =>
                  {
                      var  richTextBox = (RichTextBox)obj;
                      richTextBox.Document = e.NewValue as EnabledFlowDocument;
                  }
              });
    }

}

