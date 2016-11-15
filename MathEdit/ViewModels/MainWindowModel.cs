using MathEdit.Services;
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
        public ICommand _SaveCommand { get; set; }
        public ICommand _OpenCommand { get; set; }
        public string fileName { get; set; }
        public EnabledFlowDocument fd { get; set; }
        public bool isSaving { get; set; }


        public MainWindowModel()
        {
            this._SaveCommand = new AsyncRelayCommand<object>(this.SaveCommand, (a) => { return !this.isSaving; });
            //this._OpenCommand = new RelayCommand<object>(this.OpenCommand);
        }

        private void OpenCommand(object sender)
        {
            // needs work
            DocumentHelper helper = new DocumentHelper();
            fd = helper.openFile();
        }

        private void SaveCommand(object sender)
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

        private void SaveAsCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DocumentHelper helper = new DocumentHelper();
            helper.saveDocAs(fd);
        }
    }
}
