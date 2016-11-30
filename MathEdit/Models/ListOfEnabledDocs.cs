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
            while (reader.IsStartElement("EnabledFlowDocument"))
            {

                EnabledFlowDocument doc = new EnabledFlowDocument("");
                doc.childrenOperations.ReadXml(reader);
                reader.ReadEndElement();
                reader.ReadEndElement();
                this.Add(doc);

            }

        }

        public void WriteXml(XmlWriter writer)
        {
            myStr = null;
            foreach (EnabledFlowDocument doc in this)
            {
                writer.WriteStartElement("EnabledFlowDocument");
                writer.WriteAttributeString("Data", "Hello");
                doc.childrenOperations.WriteXml(writer);
                writer.WriteEndElement();
                writer.WriteEndElement();
            }
        }

        #endregion

    }
}
