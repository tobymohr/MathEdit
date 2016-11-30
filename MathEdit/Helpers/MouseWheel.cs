using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Markup;

namespace MathEdit.Helpers
{
    public class MouseWheel : MarkupExtension
    {
        public MouseWheelDirection Direction { get; set; }

        public MouseWheel()
        {
            Direction = MouseWheelDirection.Up;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return new MouseWheelGesture(ModifierKeys.Control, Direction);
        }
    }
}
