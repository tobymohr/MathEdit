using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MathEdit.ModelHelpers
{
    public abstract class NotifyBase : INotifyPropertyChanged
    {
        internal void RaisePropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public void SetProperty<T>(ref T member, T value, [CallerMemberName] string propertyName = null)
        {
            if (member == null || !member.Equals(value))
            {
                member = value;
                this.RaisePropertyChanged(propertyName);
            }
        }
    }
}
