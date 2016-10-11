using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MathEdit.Models
{
    public static class KeyCommands
    {
        public static RoutedUICommand Exit = new RoutedUICommand
            (
            "Exit",
            "Exit",
            typeof(KeyCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.F4, ModifierKeys.Alt)
            }
            );
    }
}
