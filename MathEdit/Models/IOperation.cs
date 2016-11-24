using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MathEdit.Models
{
    interface IOperation : INotifyPropertyChanged
    {
        #region public methods
        double getWidth();
        double setWidth(double width);
        void onIOperationChanged();
        List<EnabledFlowDocument> getChildren();
        #endregion
    }
}
