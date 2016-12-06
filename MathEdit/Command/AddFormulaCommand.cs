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
        
        public AddFormulaCommand(ObservableCollection<Operation> _formulas, Operation _formula)
        {
            formulas = _formulas;
            formula = _formula;
        }

        #endregion

        #region Methods
        
        public void Execute()
        {
            formulas.Add(formula);
        }

        public void UnExecute()
        {
            formulas.Remove(formula);
        }

        #endregion
    }
}
