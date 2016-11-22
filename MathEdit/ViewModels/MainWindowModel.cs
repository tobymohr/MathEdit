using MathEdit.Helpers;
using MathEdit.Models;
using MathEdit.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace MathEdit.ViewModels
{
    class MainWindowModel : ViewModelBase
    {
        public ICommand SaveCommand { get; set; }
        public ICommand OpenCommand { get; set; }
        public ICommand SaveAsCommand { get; set; }
        public ICommand OpenHotkeysCommand { get; set; }
        public ICommand OpenSettingsCommand { get; set; }
        public ICommand CreateNewRTBCommand { get; set; }
        public ICommand CreateNewFractionCommand { get; set; }
        public ICommand ToggleBold { get; set; }
        public ICommand ToggleItalic { get; set; }
        public ICommand ChangeFontSize { get; set; }

        public string fileName { get; set; }
        public EnabledFlowDocument flowDoc;
        public HotkeyMenu hotKeys { get; set; }
        public bool isSaving { get; set; }
        public RichTextBox parentTb { get; set; }
        public int rtbCount { get; set; }
        public double minWidth { get; set; }
        public MainWindow focusedObj;
        private string fontSize;
        private int fontSizeIndex;
        private bool isBoldChecked;
        private bool isItalicChecked;


        public MainWindowModel()
        {
            this.SaveCommand = new AsyncRelayCommand<object>(this.saveDoc, (a) => { return !this.isSaving; });
            this.OpenCommand = new RelayCommand<object>(this.openDoc);
            this.SaveAsCommand = new RelayCommand<object>(this.saveAsDoc);
            this.OpenHotkeysCommand = new RelayCommand<object>(this.openHotKeys);
            this.OpenSettingsCommand = new RelayCommand<object>(this.openSettings);
            this.CreateNewRTBCommand = new RelayCommand<object>(this.createNewRtb);
            this.CreateNewFractionCommand = new RelayCommand<object>(this.createFractionRtb);
            this.ToggleBold = new RelayCommand<object>(this.bold_Click);
            this.ToggleItalic = new RelayCommand<object>(this.italic_Click);
            this.ChangeFontSize = new RelayCommand<object>(this.changeFontSize);
            focusedObj =(MainWindow) System.Windows.Application.Current.MainWindow;
            rtbCount = 0;
            minWidth = 0;
            fontSizeIndex = 2;
            isBoldChecked = false;
            isItalicChecked = false;
        }

        #region PropertyFields
        public EnabledFlowDocument MainFlowDocument
        {
            get { return this.flowDoc; }
            set { this.SetProperty(ref this.flowDoc, value); }
        }


        public string FontSize
        {
            get { return this.fontSize; }
            set { this.SetProperty(ref this.fontSize, value); }
        }

        public int FontSizeIndex
        {
            get { return this.fontSizeIndex; }
            set { this.SetProperty(ref this.fontSizeIndex, value); }
        }

        public bool IsBoldChecked
        {
            get { return this.isBoldChecked; }
            set { this.SetProperty(ref this.isBoldChecked, value); }
        }

        public bool IsItalicChecked
        {
            get { return this.isItalicChecked; }
            set { this.SetProperty(ref this.isItalicChecked, value); }
        }

        #endregion

        #region Generic calls
      

        private RichTextBox findParent(DependencyObject sender)
        {
            if (sender != null)
            {
                DependencyObject parentObject = VisualTreeHelper.GetParent(sender);

                RichTextBox parentBox = parentObject as RichTextBox;
                if (parentBox != null)
                {
                    return parentBox;
                }
                return findParent(parentObject);
            }
            return null;
        }

        private void onTextChanged(object sender, EventArgs e)
        {
            RichTextBox rt = (RichTextBox)sender;
            if (rt.IsFocused)
            {
                double newWidth = rt.Document.GetFormattedText().WidthIncludingTrailingWhitespace;
                if (newWidth > minWidth)
                {
                    minWidth = newWidth;
                    rt.Width = minWidth;
                }
            }

            RichTextBox parent = findParent(rt);
            while (parent != null)
            {
                if (parent != null && parent != sender)
                {
                    if (parent.Name != "textBoxMain")
                    {
                        parent.Width = minWidth + 20;
                    }
                }
                parent = findParent(parent);
            }

        }

        #endregion

        #region Menu Item calls
        private void openHotKeys(object sender)
        {
            hotKeys.Visibility = Visibility.Visible;
        }

        private void createNewRtb2(object sender)
        {
            object rtb = new RichTextBoxModel();
            // hent element tilhørende focus (parent)
            // smid rtb ind
        }


        private void createNewRtb(object sender)
        {
            Paragraph para = null;
            IInputElement focusedControl = FocusManager.GetFocusedElement(focusedObj);
            parentTb = (RichTextBox)focusedControl;
            FlowDocument parentFd;

            if (parentTb.Document.GetType() == typeof(EnabledFlowDocument))
            {
                parentFd = parentTb.Document;
            }
            else
            {
                parentFd = new EnabledFlowDocument();
                parentTb.Document = parentFd;
            }

            if (parentFd.Blocks.Count == 0)
            {
                para = new Paragraph();
                parentFd.Blocks.Add(para);
                string text = String.Join(String.Empty, para.Inlines.Select(line => line.ContentStart.GetTextInRun(LogicalDirection.Forward)));
                para.Inlines.Add(text);
            }
            else if (parentFd.Blocks.Count > 0)
            {
                para = (Paragraph)parentFd.Blocks.LastBlock;
            }

            RichTextBox rtb = new RichTextBox() { Focusable = true };

            rtb.Focusable = true;
            rtb.Focus();
            rtb.TextChanged += onTextChanged;
            rtb.Document = new EnabledFlowDocument();
            Paragraph insideParagraph = new Paragraph();

            rtb.BorderThickness = new Thickness(0);
            rtb.Focus();
            FractionControl fControl = new FractionControl();
            SquareControl sControl = new SquareControl();
            PowControl pControl = new PowControl();
            rtb.Width = pControl.Width;
            insideParagraph.Inlines.Add(pControl);
            rtb.Document.Blocks.Add(insideParagraph);
            para.Inlines.Add(rtb);

        }

        private void createFractionRtb(object sender)
        {
            Paragraph para = null;
            IInputElement focusedControl = FocusManager.GetFocusedElement(focusedObj);
            parentTb = (RichTextBox)focusedControl;
            FlowDocument parentFd;

            if (parentTb.Document.GetType() == typeof(EnabledFlowDocument))
            {

                parentFd = parentTb.Document;
            }
            else
            {
                parentFd = new EnabledFlowDocument();
                parentTb.Document = parentFd;
            }

            if (parentFd.Blocks.Count == 0)
            {
                para = new Paragraph();
                parentFd.Blocks.Add(para);
                string text = String.Join(String.Empty, para.Inlines.Select(line => line.ContentStart.GetTextInRun(LogicalDirection.Forward)));
                para.Inlines.Add(text);
            }

            else if (parentFd.Blocks.Count > 0)
            {
                para = (Paragraph)parentFd.Blocks.LastBlock;
            }

            RichTextBox rtb = new RichTextBox() { Focusable = true };
            rtb.Focusable = true;
            rtb.Focus();
            rtbCount++;
            rtb.Name = "xx" + rtbCount + "xxx";
            rtb.TextChanged += onTextChanged;
            rtb.Document = new EnabledFlowDocument();
            Paragraph insideParagraph = new Paragraph();

            rtb.BorderThickness = new Thickness(0);
            rtb.Focus();
            FractionControl fControl = new FractionControl();
            rtb.Width = fControl.Width;
            insideParagraph.Inlines.Add(fControl);
            rtb.Document.Blocks.Add(insideParagraph);
            para.Inlines.Add(rtb);

        }

        private void changeFontSize(object sender)
        {

            ComboBox comboBox = (ComboBox)sender;
            string value = (string)comboBox.SelectedValue;
            if (comboBox.IsDropDownOpen)
            {
                parentTb = (RichTextBox)FocusManager.GetFocusedElement(focusedObj);
                TextSelection text = parentTb.Selection;
                parentTb.Focus();
                text.ApplyPropertyValue(RichTextBox.FontSizeProperty, value);
            }

        }

        private void textBoxMain_SelectionChanged(object sender)
        {
            //Changes combobox to
            parentTb = (RichTextBox)FocusManager.GetFocusedElement(focusedObj);
            TextSelection text = parentTb.Selection;
            object trsize = text.GetPropertyValue(RichTextBox.FontSizeProperty);
            int fs;
            if (Int32.TryParse(trsize.ToString(), out fs))
            {
                FontSize = trsize.ToString();
            }
            else
            {
                FontSizeIndex = -1;
            }
            //Changes Italic checked
            try
            {
                FontStyle italic = (FontStyle)text.GetPropertyValue(RichTextBox.FontStyleProperty);
                if (italic == FontStyles.Normal)
                {
                    IsItalicChecked = false;
                }
                else
                {
                    IsItalicChecked = true;
                }
            }
            catch (Exception)
            {
                IsItalicChecked = false;
            }
            //Changes Bold checked
            //Changes Italic checked
            try
            {
                FontWeight bold = (FontWeight)text.GetPropertyValue(RichTextBox.FontWeightProperty);
                if (bold == FontWeights.Normal)
                {
                    IsBoldChecked = false;
                }
                else
                {
                    IsBoldChecked = true;
                }
            }
            catch (Exception)
            {
                IsBoldChecked = false;
            }
            //RichTextBox rtb = (RichTextBox)sender;
            //TextPointer tp = rtb.GetPositionFromPoint()
        }

        private void bold_Click(object sender)
        {
            parentTb = (RichTextBox)FocusManager.GetFocusedElement(focusedObj);
            TextSelection text = parentTb.Selection;
            parentTb.Focus();
            if (IsBoldChecked)
            {
                text.ApplyPropertyValue(RichTextBox.FontWeightProperty, FontWeights.UltraBold);
            }
            else
            {
                text.ApplyPropertyValue(RichTextBox.FontWeightProperty, FontWeights.Normal);
            }
        }

        private void italic_Click(object sender)
        {
                parentTb = (RichTextBox)FocusManager.GetFocusedElement(focusedObj);
                TextSelection text = parentTb.Selection;
                parentTb.Focus();
                if (IsItalicChecked)
                {
                    text.ApplyPropertyValue(RichTextBox.FontStyleProperty, FontStyles.Oblique);
                }
                else
                {
                    text.ApplyPropertyValue(RichTextBox.FontStyleProperty, FontStyles.Normal);
                }
        }

        private void openSettings(object sender)
        {
            System.Diagnostics.Debug.WriteLine("du trykkede på settings");
        }
        #endregion

        #region Services
        private void openDoc(object sender)
        {
            // needs work
            DocumentHelper helper = new DocumentHelper();
            FlowDoc = helper.openFile();
        }

        private void saveDoc(object sender)
        {
            // works
            DocumentHelper helper = new DocumentHelper();
            if (fileName == null || fileName == "")
            {
                fileName = helper.saveDoc(flowDoc);
            }
            else
            {
                helper.saveDoc(flowDoc, fileName);
            }
        }

        private void saveAsDoc(object sender)
        {
            DocumentHelper helper = new DocumentHelper();
            helper.saveDocAs(flowDoc);
        }
        #endregion
    }
}
