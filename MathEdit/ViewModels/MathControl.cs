using MathEdit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MathEdit.ViewModels
{
    public interface MathControl
    {
         Operation model { get; set; }
    }
}
