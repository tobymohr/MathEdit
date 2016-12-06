using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MathEdit.Helpers
{
    //The undoRedoObject for recreating/delete
    //UserControls by the UndoRedo class
    class UndoRedoObject
    {
        public UserControl Uc { get; set; }
        public bool Deleted { get; set; }

        public UndoRedoObject(UserControl uc, bool delete)
        {
            Uc = uc;
            Deleted = delete;
        }
    }
}
