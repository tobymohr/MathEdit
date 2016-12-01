using MathEdit.Helpers;
using MathEdit.Model;
using System.Collections.ObjectModel;

namespace MathEdit.Command
{
    public class AddFormulaCommand : IUndoRedoCommand
    {
        // Regions can be used to make code foldable (minus/plus sign to the left).
        #region Fields

        // The 'shapes' field holds the current collection of shapes, 
        //  and the reference points to the same collection as the one the MainViewModel point to, 
        //  therefore when this collection is changed in a object of this class, 
        //  it also changes the collection that the MainViewModel uses.
        // For a description of an ObservableCollection see the MainViewModel class.
        private ObservableCollection<Operation> formulas;
        // The 'shape' field holds a new shape, that is added to the 'shapes' collection, 
        //  and if undone, it is removed from the collection.
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
