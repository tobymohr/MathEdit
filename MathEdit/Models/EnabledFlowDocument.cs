using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Xml;
using System.Xml.Serialization;

namespace MathEdit.Models
{
    [Serializable]
    public class EnabledFlowDocument : FlowDocument
    {
        #region Fields
        [XmlIgnore]
        public ListOfOperations childrenOperations;
        #endregion

        public EnabledFlowDocument()
        {
            childrenOperations = new ListOfOperations();
        }

        #region public methods
        #endregion


        // Enabled functionality for nested flowdocuments, paragraphs etc.
        protected override bool IsEnabledCore
        {
            get
            {
                return true;
            }
        }

        public class ListOfOperations : List<Operation>, IXmlSerializable
        {
            public ListOfOperations() : base() { }

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
                    this.Add((Operation)serial.Deserialize(reader));
                    reader.ReadEndElement();
                }
                reader.ReadEndElement();
            }

            public void WriteXml(XmlWriter writer)
            {
                foreach (Operation op in this)
                {
                    writer.WriteStartElement("EnabledFlowDocument");
                    writer.WriteAttributeString("AssemblyQualifiedName", op.GetType().AssemblyQualifiedName);
                    XmlSerializer xmlSerializer = new XmlSerializer(op.GetType());
                    xmlSerializer.Serialize(writer, op);
                    writer.WriteEndElement();
                }
            }
            #endregion
        }
    }
}
