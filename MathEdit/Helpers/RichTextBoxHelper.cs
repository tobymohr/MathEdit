﻿using MathEdit.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace MathEdit.Helpers
{
    public class RichTextBoxHelper : DependencyObject
    {
        public static EnabledFlowDocument GetDocumentXaml(DependencyObject obj)
        {
            return (EnabledFlowDocument)obj.GetValue(DocumentXamlProperty);
        }
        public static void SetDocumentXaml(DependencyObject obj, EnabledFlowDocument value)
        {
            obj.SetValue(DocumentXamlProperty, value);
        }

        public static readonly DependencyProperty DocumentXamlProperty =
            DependencyProperty.RegisterAttached(
              "DocumentXaml",
              typeof(EnabledFlowDocument),
              typeof(RichTextBoxHelper),
              new FrameworkPropertyMetadata
              {
                  BindsTwoWayByDefault = true,
                  PropertyChangedCallback = (obj, e) =>
                  {
                      var  richTextBox = (RichTextBox)obj;
                      richTextBox.Document = e.NewValue as EnabledFlowDocument;
                  }
              });
    }

}

