using MathEdit.Helpers;
using MathEdit.Models;
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
        public ICommand CreateFractionCommand { get; set; }
        public ICommand CreatePowCommand { get; set; }
        public ICommand CreateSqrtCommand { get; set; }
        public ICommand OpenCommand { get; set; }
        public ICommand SaveAsCommand { get; set; }
        public ICommand OpenHotkeysCommand { get; set; }
        public ICommand OpenSettingsCommand { get; set; }
        public ICommand CreateNewRTBCommand { get; set; }
 
        public ICommand ToggleBold { get; set; }
        public ICommand ToggleItalic { get; set; }
        public ICommand ChangeFontSize { get; set; }
        public ICommand TextBoxMainSelectionChanged { get; set; }
        public ICommand ScrollZoom { get; set; }

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
        private bool dropDownOpen;
        private Visibility visibility;
        private int count = 0;
        private double zoomValue;


        public MainWindowModel()
        {
            this.SaveCommand = new AsyncRelayCommand<object>(this.saveDoc, (a) => { return !this.isSaving; });
            this.OpenCommand = new RelayCommand<object>(this.openDoc);
            this.SaveAsCommand = new RelayCommand<object>(this.saveAsDoc);
            this.OpenHotkeysCommand = new RelayCommand<object>(this.openHotKeys);
            this.OpenSettingsCommand = new RelayCommand<object>(this.openSettings);
            this.ToggleBold = new RelayCommand<object>(this.bold_Click);
            this.ToggleItalic = new RelayCommand<object>(this.italic_Click);
            this.ChangeFontSize = new RelayCommand<object>(this.changeFontSize);
            this.CreateFractionCommand = new RelayCommand<object>(this.createFraction);
            this.CreatePowCommand = new RelayCommand<object>(this.createPow);
            this.CreateSqrtCommand = new RelayCommand<object>(this.createSquared);
            this.TextBoxMainSelectionChanged = new RelayCommand<object>(this.textBoxMain_SelectionChanged);
            this.ScrollZoom = new RelayCommand<object>(this.scrollZoom);
            focusedObj =(MainWindow) System.Windows.Application.Current.MainWindow;
            rtbCount = 0;
            minWidth = 0;
            fontSizeIndex = 1;
            isBoldChecked = false;
            isItalicChecked = false;
            visibility = Visibility.Collapsed;
            zoomValue = 1;
        }

        #region PropertyFields
        public EnabledFlowDocument MainFlowDocument
        {
            get { return this.flowDoc; }
            set { this.SetProperty(ref this.flowDoc, value); }
        }

        public Visibility Visibility
        {
            get { return this.visibility; }
            set { this.SetProperty(ref this.visibility, value); }
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

        public bool FontDropOpen
        {
            get { return this.dropDownOpen; }
            set { this.SetProperty(ref this.dropDownOpen, value); }
        }

        public double ZoomValue
        {
            get { return this.zoomValue; }
            set { this.SetProperty(ref this.zoomValue, value); }
        }

        #endregion

        #region Generic calls
        #endregion

        #region hotKey calls
        private void openHotKeys(object sender)
        {
            if (Visibility == Visibility.Visible)
            {
                Visibility = Visibility.Collapsed;
            }
            else
            {
                Visibility = Visibility.Visible;
            }
        }
        private void createFraction(object sender)
        {
            createNewFractionControl();
        }

        private void createPow(object sender)
        {
            createNewPowControl();
        }

        private void createSquared(object sender)
        {
            createNewSqrtControl();
        }

        private void scrollZoom(object sender)
        {
            if(ZoomValue < 10)
            {
                ZoomValue = ZoomValue + 0.5;

            }
            
        }
        #endregion

        #region Menu Item calls


        private Paragraph getCorrectParagraph(RichTextBox parentTb)
        {
           
            FlowDocument parentFd;
            Paragraph para = null;
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
            return para;
        }

        private void createNewFractionControl()
        {
            Paragraph para = null;
            IInputElement focusedControl = FocusManager.GetFocusedElement(focusedObj);
            RichTextBox parentBox = focusedControl as RichTextBox;
            para = getCorrectParagraph(parentBox);
            EnabledFlowDocument parentFd = parentBox.Document as EnabledFlowDocument;
            FractionControl fControl = new FractionControl();
            fControl.model.id = count++;
            parentFd.childrenOperations.Add(fControl.model);
            para.Inlines.Add(fControl);
        }

        private void createNewPowControl()
        {
            Paragraph para = null;
            IInputElement focusedControl = FocusManager.GetFocusedElement(focusedObj);
            RichTextBox parentBox = focusedControl as RichTextBox;
            para = getCorrectParagraph(parentBox);
            EnabledFlowDocument parentFd = parentBox.Document as EnabledFlowDocument;
            PowControl pControl = new PowControl();
            parentFd.childrenOperations.Add(pControl.model);
            para.Inlines.Add(pControl);
        }

        private void createNewSqrtControl()
        {
            Paragraph para = null;
            IInputElement focusedControl = FocusManager.GetFocusedElement(focusedObj);
            RichTextBox parentBox = focusedControl as RichTextBox;
            para = getCorrectParagraph(parentBox);
            EnabledFlowDocument parentFd = parentBox.Document as EnabledFlowDocument;
            SquareControl sControl = new SquareControl();
            parentFd.childrenOperations.Add(sControl.model);
            para.Inlines.Add(sControl);
        }

        private void changeFontSize(object sender)
        {
            if (dropDownOpen)
            {
                parentTb = (RichTextBox)FocusManager.GetFocusedElement(focusedObj);
                TextSelection text = parentTb.Selection;
                parentTb.Focus();
                text.ApplyPropertyValue(RichTextBox.FontSizeProperty, fontSize);
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
            flowDoc = helper.openFile();
        }

        private void saveDoc(object sender)
        {
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
