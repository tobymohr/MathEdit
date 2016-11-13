using MathEdit.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MathEdit.ViewModel
{
    class MainWindowModel
    {
        public ICommand SaveCommand { get; set; }
        public string fileName { get; set; }
        public bool isSaving { get; set; }

        //public MainWindowModel
        //{
        //    this.SaveCommand = new AsyncRelayCommand<object>(this.SaveCommandBinding_Executed, (a) => { return !this.isSaving; });
        //}

        //private void OpenCommandBinding_Executed(object sender, RoutedEventArgs e)
        //{
        //    // needs work
        //    DocumentHelper helper = new DocumentHelper();
        //    fd = helper.openFile();
        //}

        //private void SaveCommandBinding_Executed(object sender)
        //{
        //    // works
        //    DocumentHelper helper = new DocumentHelper();
        //    if (filename == "")
        //    {
        //        helper.saveDoc(fd);
        //    }
        //    else
        //    {
        //        helper.saveDoc(fd, filename);
        //    }
        //}

        //private void SaveAsCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        //{
        //    DocumentHelper helper = new DocumentHelper();
        //    helper.saveDocAs(fd);
        //}
    }
}
