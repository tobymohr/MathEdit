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

namespace MathEdit.Services
{
    class DocumentHelper
    {

        public void saveDoc(EnabledFlowDocument fd)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.FileName = "Sheet";
            saveDialog.DefaultExt = ".xml";
            saveDialog.Filter = "XML Files|*.xml";

            Nullable<bool> result = saveDialog.ShowDialog();
            if (result == true)
            {
                // no working right now
                string filename = saveDialog.FileName;
                using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    TextRange textRange = new TextRange(fd.ContentStart, fd.ContentEnd);
                    textRange.Save(fs, DataFormats.Xaml);
                }
            }
        }

        public void saveDoc(EnabledFlowDocument fd, string filename)
        {
            Monitor.Enter(this);
            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write))
            {
                TextRange textRange = new TextRange(fd.ContentStart, fd.ContentEnd);
                textRange.Save(fs, DataFormats.Xaml);
            }
            Monitor.Exit(this);
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
