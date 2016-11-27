using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media;

namespace MathEdit.ViewModels
{
    // Generic ViewModelBase
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        internal void RaisePropertyChanged(string prop)
        {
            if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs(prop)); }
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

