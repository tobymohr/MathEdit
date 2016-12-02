using MathEdit.ModelHelpers;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Xml.Serialization;

namespace MathEdit.Model
{
    [Serializable]
    public class FractionModel : Operation
    {
        [XmlIgnore]
        public ICommand ChangeWidth { get; set; }
        [XmlIgnore]
        private CustomFlowdoc numerator;
        private CustomFlowdoc denominator;
        private ListOfDocs docs;
        
        public FractionModel()
        {
            Width = 70;
            Numerator = new CustomFlowdoc();
            Denominator = new CustomFlowdoc();
            docs = new ListOfDocs() { Numerator, Denominator };
            ChangeWidth = new RelayCommand<object>(this.changeWidth);
            
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
                return width;
            }

            set
            {
                this.SetProperty(ref width, value);
            }
        }

        private void changeWidth(object sender)
        {
            Console.WriteLine("luluulul");
            Console.WriteLine(Numerator.GetFormattedText().Text);
            Width = Math.Max(Numerator.GetFormattedText().WidthIncludingTrailingWhitespace,
                    Denominator.GetFormattedText().WidthIncludingTrailingWhitespace) + 20;
        }


    }
}
