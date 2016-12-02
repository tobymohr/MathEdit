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
    public class PowModel : Operation
    {
        private string number;
        private string power;
        [XmlIgnore]
        public ICommand ChangeWidth { get; set; }
        public PowModel()
        {
            Width = 70;
        }

        public string Number
        {
            get { return number; }
            set { this.SetProperty(ref number, value); }
        }
        public string Power
        {
            get { return power; }
            set { this.SetProperty(ref power, value); }
        }

        public override double Width
        {
            get
            {
                return width;
                double calcedWidth = 80;
                return calcedWidth;
            }

            set
            {
                this.SetProperty(ref width, value);
            }
        }
    }
}
