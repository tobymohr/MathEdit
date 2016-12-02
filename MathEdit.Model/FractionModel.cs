using MathEdit.ModelHelpers;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Xml.Serialization;

namespace MathEdit.Model
{
    [Serializable]
    public class FractionModel : Operation
    {
        private CustomFlowdoc numerator;
        private CustomFlowdoc denominator;
        private ListOfDocs docs;
        
        public FractionModel()
        {
            Width = 70;
            Numerator = new CustomFlowdoc();
            Denominator = new CustomFlowdoc();
            docs = new ListOfDocs() { Numerator, Denominator };
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
        public CustomFlowdoc Numerator
        {
            get { return numerator; }
            set { this.SetProperty(ref numerator, value); }
        }

        [XmlIgnore]
        public CustomFlowdoc Denominator
        {
            get { return denominator; }
            set { this.SetProperty(ref denominator, value); }
        }


        
        public override double Width
        {
            get
            {
                double calcedWidth = Math.Max(Numerator.GetFormattedText().WidthIncludingTrailingWhitespace,
                    Denominator.GetFormattedText().WidthIncludingTrailingWhitespace) + 30;
                return calcedWidth;
            }

            set
            {
                this.SetProperty(ref width, value);
            }
        }
    }
}
