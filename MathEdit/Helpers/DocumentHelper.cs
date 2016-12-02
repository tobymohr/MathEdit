using MathEdit.Model;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

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

        public ObservableCollection<Operation> openFile()
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.DefaultExt = ".xml";
            openDialog.Filter = "XML Files|*.xml";
            Nullable<bool> result = openDialog.ShowDialog();
            ObservableCollection<Operation> formulas = new ObservableCollection<Operation>();
            if (result == true)
            {

                XmlSerializer xs = new XmlSerializer(typeof(ObservableCollection<Operation>));
                using (StreamReader rd = new StreamReader(openDialog.FileName))
                {
                    formulas = xs.Deserialize(rd) as ObservableCollection<Operation>;
                }
                return formulas;
            }
            else
            {
                return null;
            }
        }


    }

}
