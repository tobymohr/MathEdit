﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MathEdit.Models
{
    public interface IOperation : INotifyPropertyChanged
    {
        #region public methods
        void onIOperationChanged();
        double width { get; set; }
        List<EnabledFlowDocument> getBoxes();
        #endregion
    }
}
