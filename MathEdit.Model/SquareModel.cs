using MathEdit.ModelHelpers;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Xml.Serialization;

namespace MathEdit.Model
{
    [Serializable]
    public class SquareModel : Operation
    {
        private CustomFlowdoc number;
        private ListOfDocs docs;

        public SquareModel()
        {

        }

        public SquareModel(string id)
        {
            Width = 70;
            Number = new CustomFlowdoc();
            docs = new ListOfDocs() { Number };
        }
        

        public override ListOfDocs ListOfDocs
        {
            get
            {
                return docs;
            }

            set
            {
                docs = value;
            }
        }

        [XmlIgnore]
        public CustomFlowdoc Number
        {
            get { return number; }
            set { this.SetProperty(ref number, value); }
        }

        public override double Width
        {
            get
            {
                double calcedWidth = Number.GetFormattedText().WidthIncludingTrailingWhitespace + 30;
                return calcedWidth;
            }

            set
            {
                this.SetProperty(ref width, value);
            }
        }
    }
}
