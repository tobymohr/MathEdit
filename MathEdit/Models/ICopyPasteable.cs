using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathEdit.Models
{
    public interface ICopyPasteable
    {
        void CopyToClipboard();
        void PasteFromClipboard();
    }

}
