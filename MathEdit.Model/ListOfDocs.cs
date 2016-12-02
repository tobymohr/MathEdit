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
    public class ListOfDocs : List<CustomFlowdoc>, IXmlSerializable
    {
        private string dataFormat = DataFormats.XamlPackage;
        public ListOfDocs() : base() { }

        #region IXmlSerializable
        public System.Xml.Schema.XmlSchema GetSchema() { return null; }
        public void ReadXml(XmlReader reader)
        {
            reader.ReadStartElement("ListOfDocs");
            while (reader.IsStartElement("CustomFlowdoc"))
            {
                CustomFlowdoc doc = new CustomFlowdoc() ;
                string text = reader.GetAttribute("Data");
                this.Add(doc);
                reader.ReadEndElement();
            }
            reader.ReadEndElement();
        }

        public void WriteXml(XmlWriter writer)
        {
            foreach (CustomFlowdoc doc in this)
            {
                writer.WriteStartElement("CustomFlowdoc");
                writer.WriteAttributeString("Data", doc.GetFormattedText().Text);
                writer.WriteEndElement();
            }
        }

        #endregion

    }
}