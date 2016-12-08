﻿using MathEdit.Helpers;
using MathEdit.Model;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System;
using MathEdit.ViewModels;

namespace MathEdit.Views
{
    /// <summary>
    /// Interaction logic for FractionControl.xaml
    /// </summary>
    public partial class FractionControl : UserControl, MathControl
    {
        private Operation model;

        Operation MathControl.model
        {
            get
            {
                return model;
            }

            set
            {
                this.model = value;
            }
        }

        public FractionControl(EnabledFlowDocument parent)
        {
            model = new FractionModel(parent);
            InitializeComponent();
            numenatorTextBox.Document = model.ListOfEnabledDocs.ElementAt(0);
            denumenatorTextBox.Document = model.ListOfEnabledDocs.ElementAt(1);
            numenatorTextBox.TextChanged += onChange;
            denumenatorTextBox.TextChanged += onChange;
            setUIWidth();
        }

        public void onChange(object sender, RoutedEventArgs e)
        {
            setUIWidth();
        }

        public void setUIWidth()
        {
            FractionModel model = (FractionModel)this.model;
            denumenatorTextBox.Width = model.denumenatorWidth;
            numenatorTextBox.Width = model.numenatorWidth;
            TrackSurface.Width = model.outerWidth;
        }
    }
}
