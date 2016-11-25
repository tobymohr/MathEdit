using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MathEdit.Models
{

    [Serializable]
    [XmlInclude(typeof(FractionModel))]
    [XmlInclude(typeof(PowModel))]
    [XmlInclude(typeof(SquareModel))]
    public abstract class Operation
    {
        [XmlAttribute("width")]
        abstract public double width { get; set; }
        [XmlAttribute("boxes")]
        abstract public List<EnabledFlowDocument> boxes { get; set; }
        [XmlAttribute("outerWidth")]
        abstract public double outerWidth { get; set; }
    }
}
