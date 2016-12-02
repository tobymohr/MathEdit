using MathEdit.ModelHelpers;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Xml.Serialization;

namespace MathEdit.Model
{
    [Serializable]
    public class PowModel : Operation
    {
        private ListOfDocs docs;
        private CustomFlowdoc number;
        private CustomFlowdoc power;

        public PowModel()
        {
            Width = 70;
            Number = new CustomFlowdoc();
            Power = new CustomFlowdoc();
            docs = new ListOfDocs() { Number, Power };
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
        [XmlIgnore]
        public CustomFlowdoc Power
        {
            get { return power; }
            set { this.SetProperty(ref power, value); }
        }

        public override double Width
        {
            get
            {
                return width;
                double calcedWidth = Number.GetFormattedText().WidthIncludingTrailingWhitespace + Power.GetFormattedText().WidthIncludingTrailingWhitespace + 30;
                return calcedWidth;
            }

            set
            {
                this.SetProperty(ref width, value);
            }
        }


    }
}
