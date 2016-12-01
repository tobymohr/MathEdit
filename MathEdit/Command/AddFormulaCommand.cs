using MathEdit.Helpers;
using MathEdit.Model;
using System.Collections.ObjectModel;

namespace MathEdit.Command
{
    public class AddFormulaCommand : IUndoRedoCommand
    {
        #region Fields

        private ObservableCollection<FractionModel> formulas;
        private FractionModel formula;

        #endregion

        #region Constructor

        // For changing the current state of the diagram.
        public AddFormulaCommand(ObservableCollection<FractionModel> _formulas, FractionModel _formula)
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
