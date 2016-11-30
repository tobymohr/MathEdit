using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MathEdit.Helpers
{
    public enum MouseWheelDirection { Up, Down }

    class MouseWheelGesture : MouseGesture
    {
        public MouseWheelDirection Direction { get; set; }

        public MouseWheelGesture(ModifierKeys keys, MouseWheelDirection direction): base(MouseAction.WheelClick, keys)
        {
            Direction = direction;
        }

        public override bool Matches(object targetElement, InputEventArgs inputEventArgs)
        {
            if (!(inputEventArgs is MouseWheelEventArgs)) return false;
            var args = (MouseWheelEventArgs)inputEventArgs;
            if (!base.Matches(targetElement, inputEventArgs))
                return false;
            if (Direction == MouseWheelDirection.Up && args.Delta > 0
                || Direction == MouseWheelDirection.Down && args.Delta < 0)
            {
                inputEventArgs.Handled = true;
                return true;
            }

            return false;
        }

    }
    
}
