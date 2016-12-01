using MathEdit.Model;
using MathEdit.Views;
using System;
using System.Collections.Generic;

namespace MathEdit.Helpers
{
    public class Undoer
    {
        protected FractionModel txtBox;
        protected List<Operation> LastData = new List<Operation>();
        protected int undoCount = 0;

        protected bool undoing = false;
        protected bool redoing = false;


        public Undoer(FractionModel txtBox)
        {
            this.txtBox = txtBox;
            LastData.Add(txtBox);
        }

        public void undo_Click(object sender, EventArgs e)
        {
            this.Undo();
        }
        public void redo_Click(object sender, EventArgs e)
        {
            this.Redo();
        }

        public void Undo()
        {
            try
            {
                undoing = true;
                ++undoCount;
               // txtBox.model = LastData[LastData.Count - undoCount - 1];
            }
            catch { }
            finally { this.undoing = false; }
        }
        public void Redo()
        {
            try
            {
                if (undoCount == 0)
                    return;

                redoing = true;
                --undoCount;
              //  txtBox.Text = LastData[LastData.Count - undoCount - 1];
            }
            catch { }
            finally { this.redoing = false; }
        }

        public void Save()
        {
            //if (undoing || redoing)
            //    return;

            //if (LastData[LastData.Count - 1] == txtBox.Text)
            //    return;

            //LastData.Add(txtBox.Text);
            //undoCount = 0;
        }
    }
}
