using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Xml;
using System.Xml.Serialization;

namespace MathEdit.Models
{
    public class ListOfEnabledDocs : List<EnabledFlowDocument>, IXmlSerializable
    {
        public ListOfEnabledDocs() : base() { }

        #region IXmlSerializable
        public System.Xml.Schema.XmlSchema GetSchema() { return null; }

        public void ReadXml(XmlReader reader)
        {
            reader.ReadStartElement("ListOfEnabledFlowDocument");
            while (reader.IsStartElement("EnabledFlowDocument"))
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
            foreach (EnabledFlowDocument doc in this)
            {
                writer.WriteStartElement("EnabledFlowDocument");
                writer.WriteAttributeString("AssemblyQualifiedName", doc.GetType().AssemblyQualifiedName);
                MemoryStream stream = new MemoryStream();
                XamlWriter.Save(doc, stream);
                stream.Position = 0;
            }
        }
        #endregion
    }
}
