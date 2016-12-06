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
        private Stack<UndoRedoObject> undoStack = new Stack<UndoRedoObject>();
        private Stack<UndoRedoObject> redoStack = new Stack<UndoRedoObject>();
        #endregion

        public UndoRedo()
        {
            
        }

        public void Add(UserControl uc, bool delete)
        {
            redoStack.Clear();
            undoStack.Push(new UndoRedoObject(uc,delete));
        }

        public UndoRedoObject Undo()
        {
            if (!undoStack.Any()) return null;
            var uro = undoStack.Pop();
            redoStack.Push(uro);
            return uro;
        }

        public UndoRedoObject Redo()
        {
            if (!redoStack.Any()) return null;
            var uro = redoStack.Pop();
            undoStack.Push(uro);
            return uro;
        }

    }
}
