using System;
using System.Collections.ObjectModel;
using System.Windows.Documents;
using System.Xml.Serialization;
using GalaSoft.MvvmLight;
using MathEdit.ModelHelpers;

namespace MathEdit.Model
{
    [Serializable]
    public abstract class Operation : NotifyBase
    {
        protected double width;
        protected int x = 100;
        protected int y = 100;

        abstract public double Width { get; set; }
        public int X { get { return x; } set { this.SetProperty(ref x, value); } }
        public int Y { get { return y; } set { this.SetProperty(ref y, value); } }

      

    }

    
}
