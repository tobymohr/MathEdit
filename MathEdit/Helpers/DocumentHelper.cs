using MathEdit.Model;
using Microsoft.Win32;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Linq;
using System.Text;

namespace MathEdit.Helpers
{
    /* Filesystem IO */
    class DocumentHelper
    {
        public string getSaveDialog()
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.FileName = "Sheet";
            saveDialog.DefaultExt = ".xml";
            saveDialog.Filter = "XML Files|*.xml";

            bool? result = saveDialog.ShowDialog();
            if (result == true)
            {
                return saveDialog.FileName;
            }
            return null;
        }
          
        public void saveDoc(byte[] binaryFlowDocument, string fileName)
        {
            try
            {
                File.WriteAllBytes(fileName, binaryFlowDocument);
            }
            catch (IOException e)
            {
                System.Diagnostics.Debug.WriteLine("Noget gik galt i gemme processen" + e.InnerException);
            }
        }

        public EnabledFlowDocument openFile()
        {
            EnabledFlowDocument doc = null;
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.DefaultExt = ".xml";
            openDialog.Filter = "XML Files|*.xml";
            Nullable<bool> result = openDialog.ShowDialog();
            if (result == true)
            {

                byte[] content = File.ReadAllBytes(openDialog.FileName);

                using (var stream = new MemoryStream(content))
                {
                    StreamReader reader = new StreamReader(stream);
                    string text = reader.ReadToEnd();
                    var xmlSerializer = new XmlSerializer(typeof(ListOfEnabledDocs));
                    var xmlReader = XmlReader.Create(new StringReader(text));
                    ListOfEnabledDocs docs = new ListOfEnabledDocs();
                    docs.ReadXml(xmlReader);
                    doc = docs.ElementAt(0);

                }

                return doc;
            }
            else
            {
                return null;
            }
        }


    }

}
