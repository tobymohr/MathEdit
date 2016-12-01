using MathEdit.Model;
using Microsoft.Win32;
using System;
using System.IO;
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

        //public EnabledFlowDocument openFile()
        //{
        //    EnabledFlowDocument doc = new EnabledFlowDocument();
        //    ListOfDocs docs = new ListOfDocs { doc };
        //    OpenFileDialog openDialog = new OpenFileDialog();
        //    openDialog.DefaultExt = ".xml";
        //    openDialog.Filter = "XML Files|*.xml";
        //    Nullable<bool> result = openDialog.ShowDialog();
        //    if (result == true)
        //    {
                
        //        byte[] content = File.ReadAllBytes(openDialog.FileName);

        //        using (var stream = new MemoryStream(content))
        //        {
        //            docs.ReadXml(XmlReader.Create(stream));
        //        }
        //        return doc;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

       
    }

}
