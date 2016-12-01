using MathEdit.Helpers;
using MathEdit.Model;
using System.Collections.ObjectModel;

namespace MathEdit.Command
{
    public class AddFormulaCommand : IUndoRedoCommand
    {
        #region Fields

        private ObservableCollection<Operation> formulas;
        private Operation formula;

        #endregion

        #region Constructor

        // For changing the current state of the diagram.
        public AddFormulaCommand(ObservableCollection<Operation> _formulas, Operation _formula)
        {
            formulas = _formulas;
            formula = _formula;
        }

        #endregion

        #region Methods

        // For doing and redoing the command.
        public void Execute()
        {
            formulas.Add(formula);
        }

        // For undoing the command.
        public void UnExecute()
        {
            formulas.Remove(formula);
        }

        #endregion
    }
}
