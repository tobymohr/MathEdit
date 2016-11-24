using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MathEdit.Models
{
    public class CustomRichTextBox : RichTextBox
    {
        public static readonly RoutedEvent TapEvent = EventManager.RegisterRoutedEvent(
        "Tap", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(CustomRichTextBox));

        // Provide CLR accessors for the event
        public event RoutedEventHandler Tap
        {
            add { AddHandler(TapEvent, value); }
            remove { RemoveHandler(TapEvent, value); }
        }

        // This method raises the Tap event
        void RaiseTapEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(CustomRichTextBox.TapEvent);
            RaiseEvent(newEventArgs);
        }
        // For demonstration purposes we raise the event when the MyButtonSimple is clicked
        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            Console.WriteLine("hello " + this.Name);
            RaiseTapEvent();
            base.OnTextChanged(e);
        }
    }
}
