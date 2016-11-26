using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Xml;
using System.Xml.Serialization;

namespace MathEdit.Models
{
    [Serializable]
    public abstract class Operation
    {
        abstract public double width { get; set; }
        abstract public ListOfEnabledDocs boxes { get; set; }
        abstract public double outerWidth { get; set; }
    }

    
}
