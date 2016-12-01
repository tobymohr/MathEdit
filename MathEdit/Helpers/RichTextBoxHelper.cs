using MathEdit.Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

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
                      richTextBox.Document = e.NewValue as FlowDocument;
                  }
              });
    }

}