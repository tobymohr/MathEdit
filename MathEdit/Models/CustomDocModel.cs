using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEdit.Models
{
    public class CustomDocModel
    {
        private string _text;
        private ListOfOperations _childrenOperations;
        public CustomDocModel(string id)
        {
            childrenOperations = new ListOfOperations();
        }
        public CustomDocModel()
        {

        }

        public string text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
            }
        }

        public ListOfOperations childrenOperations
        {
            get
            {
                return this._childrenOperations;
            }
            set
            {
                this._childrenOperations = value;
            }
        }
    }
}
