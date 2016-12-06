using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MathEdit.Helpers
{
    
    class UndoRedo
    {
        #region Fields
        private Stack<Uro> undoStack = new Stack<Uro>();
        private Stack<Uro> redoStack = new Stack<Uro>();
        #endregion

        public UndoRedo()
        {
            
        }

        public void Add(UserControl uc, bool delete)
        {
            redoStack.Clear();
            undoStack.Push(new Uro(uc,delete));
        }

        public Uro Undo()
        {
            if (!undoStack.Any()) return null;
            var uro = undoStack.Pop();
            if (uro.Deleted)
            {
                uro.Deleted = false;
                redoStack.Push(uro);
                return uro;
            }
            else
            {
                uro.Deleted = true;
                redoStack.Push(uro);
                return uro;
            }
        }

        public Uro Redo()
        {
            if (!redoStack.Any()) return null;
            var uro = redoStack.Pop();
            if (uro.Deleted)
            {
                uro.Deleted = false;
                redoStack.Push(uro);
                return uro;
            }
            else
            {
                uro.Deleted = true;
                redoStack.Push(uro);
                return uro;
            }
        }

    }
}
