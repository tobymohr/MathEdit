using MathEdit.Model;
using MathEdit.ModelHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Xml;
using System.Xml.Serialization;

namespace MathEdit.Model
{
    public class ListOfEnabledDocs : List<EnabledFlowDocument>, IXmlSerializable
    {
        private string dataFormat = DataFormats.XamlPackage;
        public ListOfEnabledDocs() : base() { }

        #region IXmlSerializable
        public System.Xml.Schema.XmlSchema GetSchema() { return null; }
        public void ReadXml(XmlReader reader)
        {
            reader.ReadStartElement("ListOfEnabledDocs");
            while (reader.IsStartElement("EnabledFlowDocument"))
            {
                EnabledFlowDocument doc = new EnabledFlowDocument("");
                string text = reader.GetAttribute("Data");
                doc.childrenOperations.ReadXml(reader);
                this.Add(doc);
                reader.ReadEndElement();
            }
            reader.ReadEndElement();


        }

        public void WriteXml(XmlWriter writer)
        {
            foreach (EnabledFlowDocument doc in this)
            {
                writer.WriteStartElement("EnabledFlowDocument");
                writer.WriteAttributeString("Data", doc.GetFormattedText().Text);
                doc.childrenOperations.WriteXml(writer);
                writer.WriteEndElement();
                writer.WriteEndElement();
            }
        }

        #endregion

    }
}