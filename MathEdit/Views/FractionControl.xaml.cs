﻿using MathEdit.Helpers;
using MathEdit.Model;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System;

namespace MathEdit.Views
{
    /// <summary>
    /// Interaction logic for FractionControl.xaml
    /// </summary>
    public partial class FractionControl : UserControl
    {
        public FractionModel model { get; set; }

        public FractionControl(EnabledFlowDocument parent)
        {
            model = new FractionModel(parent);
            DataContext = model;
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
            Console.WriteLine("Setup UI");
            Console.WriteLine(model.ListOfEnabledDocs.ElementAt(0).text);
            Console.WriteLine(model.ListOfEnabledDocs.ElementAt(1).text);
            denumenatorTextBox.Width = model.denumenatorWidth;
            numenatorTextBox.Width = model.numenatorWidth;
            TrackSurface.Width = model.outerWidth;
        }
    }
}
