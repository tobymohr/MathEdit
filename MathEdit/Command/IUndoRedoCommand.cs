using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEdit.Helpers
{
    public interface IUndoRedoCommand
    {
        
        #region Methods (that has to be implemented)

        // For doing and redoing the command.
        void Execute();
        // For undoing the command.
        void UnExecute();

        #endregion
    }
}
