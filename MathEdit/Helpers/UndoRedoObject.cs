using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;

namespace MathEdit.Helpers
{
    //The undoRedoObject for recreating/delete
    //UserControls by the UndoRedo class
    class UndoRedoObject
    {
        public UserControl Uc { get; set; }
        public bool Deleted { get; set; }
        public TextPointer TextPointer { get; set; }

        public UndoRedoObject(UserControl uc, bool delete)
        {
            Uc = uc;
            Deleted = delete;
        }

        public UndoRedoObject(UserControl uc, bool delete, TextPointer textPointer)
        {
            Uc = uc;
            Deleted = delete;
            TextPointer = textPointer;
        }
    }
}
