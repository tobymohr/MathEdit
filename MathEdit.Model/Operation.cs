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


        private bool moving = false;
        protected double width;
        protected double x = 100;
        protected double y = 100;
        abstract public ListOfDocs ListOfDocs { get; set; }

        abstract public double Width { get; set; }
        public double X { get { return x; } set { this.SetProperty(ref x, value); } }
        public double Y { get { return y; } set { this.SetProperty(ref y, value); } }

        protected void mouseDown(Object sender)
        {
            moving = true;
        }
        protected void mouseMove(Object o)
        {
            if (moving)
            {
                var args = (MouseEventArgs) o;
                var shapeVisualElement = (FrameworkElement) args.MouseDevice.Target;
                var canvas = FindParentOfType<Canvas>(shapeVisualElement);
                var mp = Mouse.GetPosition(canvas);
                X = X + (mp.X - X)-33; //Hard coded fix, real bad TODO FIX
                Y = Y + (mp.Y - Y)-23; //Hard coded fix, real bad TODO FIX
            }
        }
        private static T FindParentOfType<T>(DependencyObject o)
        {
            dynamic parent = VisualTreeHelper.GetParent(o);
            return parent.GetType().IsAssignableFrom(typeof(T)) ? parent : FindParentOfType<T>(parent);
        }
        protected void mouseUp(Object sender)
        {
            moving = false;
        }


    }

    
}
