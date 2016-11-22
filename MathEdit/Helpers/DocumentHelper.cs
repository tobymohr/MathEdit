using MathEdit.Views;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Markup;

namespace MathEdit.Helpers
{
    /* Filesystem IO */
    class DocumentHelper
    {
        public string saveDoc(EnabledFlowDocument fd)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.FileName = "Sheet";
            saveDialog.DefaultExt = ".xml";
            saveDialog.Filter = "XML Files|*.xml";
            
            bool? result = saveDialog.ShowDialog();
            if (result == true)
            {
                // no working right now
                string filename = saveDialog.FileName;
                System.Diagnostics.Debug.WriteLine(filename);
                using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    TextRange textRange = new TextRange(fd.ContentStart, fd.ContentEnd);
                    textRange.Save(fs, DataFormats.Xaml);
                }
                return filename;
            }
            return null;
        }

        public void saveDoc(EnabledFlowDocument fd, string filename)
        {
            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write))
            {
                TextRange textRange = new TextRange(fd.ContentStart, fd.ContentEnd);
                textRange.Save(fs, DataFormats.Xaml);
            }
        }

        public void saveDocAs(EnabledFlowDocument fd)
        {

        }

        // No cigar, only ost.
        public EnabledFlowDocument openFile()
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.DefaultExt = "xml";
            openDialog.Filter = "XML Files|*.xml";

            Nullable<bool> result = openDialog.ShowDialog();
            if (result == true)
            {
                string filename = openDialog.FileName;
                EnabledFlowDocument fd = new EnabledFlowDocument();

                using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
                {
                    TextRange textRange = new TextRange(fd.ContentStart, fd.ContentEnd);
                    textRange.Load(fs, DataFormats.Xaml);
                }
                return fd;
            }
            return null;
        }
    }

}
