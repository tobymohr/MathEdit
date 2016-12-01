using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Xml;
using System.Xml.Serialization;

namespace MathEdit.Model
{
    public class ListOfDocs : List<FlowDocument>, IXmlSerializable
    {

        private string dataFormat = DataFormats.XamlPackage;
        //private string dataFormat = DataFormats.Rtf;
        public ListOfDocs() : base()
        {

        }


        #region IXmlSerializable
        public System.Xml.Schema.XmlSchema GetSchema() { return null; }

        public void ReadXml(XmlReader reader)
        {
            while (reader.IsStartElement("EnabledFlowDocument"))
            {

                FlowDocument doc = new FlowDocument();
                reader.ReadEndElement();
                reader.ReadEndElement();
                this.Add(doc);

            }

        }

        public void WriteXml(XmlWriter writer)
        {

            foreach (FlowDocument doc in this)
            {
                writer.WriteStartElement("EnabledFlowDocument");
                writer.WriteAttributeString("Data", "Hello");
                writer.WriteEndElement();
                writer.WriteEndElement();
            }
        }

        #endregion

    }
}
