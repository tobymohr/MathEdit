using MathEdit.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Xml;

namespace MathEdit.Helpers
{
    /* Filesystem IO */
    class DocumentHelper
    {
        public void saveDoc(byte[] binaryFlowDocument, string fileName)
        {
            // build document while preparing
            if (fileName != "")
            {
                File.WriteAllBytes(fileName, binaryFlowDocument);
            }
            else
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.FileName = "Sheet";
                saveDialog.DefaultExt = ".xml";
                saveDialog.Filter = "XML Files|*.xml";

                bool? result = saveDialog.ShowDialog();
                if (result == true)
                {
                    File.WriteAllBytes(saveDialog.FileName, binaryFlowDocument);
                }
            }
        }



        public void saveAsDoc(byte[] binaryFlowDocument)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.FileName = "Sheet";
            saveDialog.DefaultExt = ".xml";
            saveDialog.Filter = "XML Files|*.xml";

            bool? result = saveDialog.ShowDialog();
            if (result == true)
            {
                File.WriteAllBytes(saveDialog.FileName, binaryFlowDocument);
            }
        }

        public EnabledFlowDocument openFile()
        {
            EnabledFlowDocument doc = new EnabledFlowDocument();
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.DefaultExt = ".xml";
            openDialog.Filter = "XML Files|*.xml";
            Nullable<bool> result = openDialog.ShowDialog();
            if (result == true)
            {
                
                byte[] content = File.ReadAllBytes(openDialog.FileName);

                using (var stream = new MemoryStream(content))
                {
                    doc.childrenOperations.ReadXml(XmlReader.Create(stream));
                }
                return doc;
            }
            else
            {
                return null;
            }
        }

        private static byte[] FlowDocumentToByteArray(FlowDocument flowDocument)
        {
            using (var stream = new MemoryStream())
            {
                XamlWriter.Save(flowDocument, stream);
                stream.Position = 0;
                return stream.ToArray();
            }
        }

        private static FlowDocument FlowDocumentFromByteArray(byte[] bytes)
        {
            using (var stream = new MemoryStream(bytes))
            {
                return (EnabledFlowDocument)XamlReader.Load(stream);
            }
        }
    }

}
