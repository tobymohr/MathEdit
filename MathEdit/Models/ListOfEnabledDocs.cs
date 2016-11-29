using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Xml;
using System.Xml.Serialization;

namespace MathEdit.Models
{
    public class ListOfEnabledDocs : List<EnabledFlowDocument>, IXmlSerializable
    {

        private string dataFormat = DataFormats.XamlPackage;
        //private string dataFormat = DataFormats.Rtf;
        public ListOfEnabledDocs() : base() { }
        public string myStr = null;
        

        #region IXmlSerializable
        public System.Xml.Schema.XmlSchema GetSchema() { return null; }

        public void ReadXml(XmlReader reader)
        {
            while (reader.IsStartElement("ListOfEnabledFlowDocument"))
            {
                Type type = Type.GetType(reader.GetAttribute("AssemblyQualifiedName"));
                XmlSerializer serial = new XmlSerializer(type);
                reader.ReadStartElement("EnabledFlowDocument");
                this.Add((EnabledFlowDocument)serial.Deserialize(reader));
                reader.ReadEndElement();
            }
            reader.ReadEndElement();

        }

        public void WriteXml(XmlWriter writer)
        {
            myStr = null;
            foreach (EnabledFlowDocument doc in this)
            {
                writer.WriteStartElement("EnabledFlowDocument");
                writer.WriteStartElement("Data");
                writer.WriteElementString("Text", doc.GetFormattedText().Text);
                doc.childrenOperations.WriteXml(writer);
                writer.WriteEndElement();
                writer.WriteEndElement();
            }
        }

        public string GetContent(FlowDocument document)
        {
            MemoryStream ms = new MemoryStream();
            TextRange tRange = new TextRange(document.ContentStart, document.ContentEnd);
            tRange.Save(ms, dataFormat);

            ms.Position = 0;

            string base64 = Convert.ToBase64String(ms.ToArray());

            return base64;
        }

        private void LoadDataIntoFlowDocument(string data, FlowDocument flowDocument)
        {
            byte[] content = Convert.FromBase64String(data);

            MemoryStream ms = new MemoryStream(content);

            ms.Position = 0;
            TextRange textRange = new TextRange(flowDocument.ContentStart, flowDocument.ContentEnd);
            textRange.Load(ms, dataFormat);
        }
        #endregion
    }
}
