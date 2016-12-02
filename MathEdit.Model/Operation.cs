using System;
using System.Collections.ObjectModel;
using System.Windows.Documents;
using System.Windows.Input;
using System.Xml.Serialization;
using GalaSoft.MvvmLight;
using MathEdit.ModelHelpers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MathEdit.Model
{
    [XmlInclude(typeof(FractionModel))]
    [XmlInclude(typeof(PowModel))]
    [XmlInclude(typeof(SquareModel))]
    [Serializable]
    public abstract class Operation : NotifyBase
    {
        public bool moving = false;
        protected double width;
        protected double x = 100;
        protected double y = 100;

        abstract public double Width { get; set; }
        public double X { get { return x; } set { this.SetProperty(ref x, value); } }
        public double Y { get { return y; } set { this.SetProperty(ref y, value); } }


    }
}
