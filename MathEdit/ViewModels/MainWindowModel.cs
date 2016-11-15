using MathEdit.Services;
using MathEdit.Services.MathEdit.Services;
using MathEdit.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MathEdit.ViewModels
{
    class MainWindowModel : ViewModelBase
    {
        public ICommand SaveCommand { get; set; }
        public ICommand OpenCommand { get; set; }
        public ICommand SaveAsCommand { get; set; }
        public string fileName { get; set; }
        public EnabledFlowDocument fd { get; set; }
        public bool isSaving { get; set; }


        public MainWindowModel()
        {
            this.SaveCommand = new AsyncRelayCommand<object>(this.SaveDoc, (a) => { return !this.isSaving; });
            this.OpenCommand = new RelayCommand<object>(this.OpenDoc);
            this.SaveAsCommand = new RelayCommand<object>(this.SaveAsDoc);
        }

        private void OpenDoc(object sender)
        {
            // needs work
            DocumentHelper helper = new DocumentHelper();
            fd = helper.openFile();
        }

        private void SaveDoc(object sender)
        {
            // works
            DocumentHelper helper = new DocumentHelper();
            if (fileName == null || fileName == "")
            {
                fileName = helper.saveDoc(fd);
            }
            else
            {
                helper.saveDoc(fd, fileName);
            }
        }

        private void SaveAsDoc(object sender)
        {
            DocumentHelper helper = new DocumentHelper();
            helper.saveDocAs(fd);
        }
    }
}
