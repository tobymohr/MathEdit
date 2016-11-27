﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace MathEdit.Models
{
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