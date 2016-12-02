using MathEdit.Command;
using MathEdit.Helpers;
using MathEdit.Model;
using MathEdit.Views;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Xml;
using System.Xml.Serialization;

namespace MathEdit.ViewModels
{
    class MainWindowModel : ViewModelBase
    {
        private UndoRedoController undoRedoController = UndoRedoController.Instance;
        public ICommand SaveCommand { get; }
        public ICommand CreateFractionCommand { get; }
        public ICommand CreatePowCommand { get; }
        public ICommand CreateSqrtCommand { get; }
        public ICommand OpenCommand { get; }
        public ICommand SaveAsCommand { get; }
        public ICommand OpenHotkeysCommand { get; }
        public ICommand OpenSettingsCommand { get; }
        public ICommand CreateNewRTBCommand { get; }
 
        public ICommand ToggleBold { get; }
        public ICommand ToggleItalic { get; }
        public ICommand ChangeFontSize { get; }
        public ICommand TextBoxMainSelectionChanged { get; }
        public ICommand ScrollIn { get; }
        public ICommand ScrollOut { get; }

        public ICommand UndoCommand { get; }
        public ICommand RedoCommand { get; }

        public string fileName { get; set; }
        public HotkeyMenu hotKeys { get; set; }
        public bool isSaving { get; set; }
        public RichTextBox parentTb { get; set; }
        public int rtbCount { get; set; }
        byte[] BinaryFlowDocument;
        public double minWidth { get; set; }
        public MainWindow focusedObj;
        private string fontSize;
        private int fontSizeIndex;
        private bool isBoldChecked;
        private Operation latestOperation;
        private bool isItalicChecked;
        private bool dropDownOpen;
        private Visibility visibility;
        public ObservableCollection<Operation> formulas { get; set; }
        private double zoomValue;


        public MainWindowModel()
        {
            formulas = new ObservableCollection<Operation>() { };
            this.SaveCommand = new RelayCommand<object>(this.saveDoc);
            this.SaveAsCommand = new RelayCommand<object>(this.saveAsDoc);
            this.OpenCommand = new RelayCommand<object>(this.openDoc);
            this.OpenHotkeysCommand = new RelayCommand<object>(this.openHotKeys);
            this.OpenSettingsCommand = new RelayCommand<object>(this.openSettings);
            this.ToggleBold = new RelayCommand<object>(this.bold_Click);
            this.ToggleItalic = new RelayCommand<object>(this.italic_Click);
            this.ChangeFontSize = new RelayCommand<object>(this.changeFontSize);
            this.CreateFractionCommand = new RelayCommand<object>(this.createFraction);
            this.CreatePowCommand = new RelayCommand<object>(this.createPow);
            this.CreateSqrtCommand = new RelayCommand<object>(this.createSquared);
            this.TextBoxMainSelectionChanged = new RelayCommand<object>(this.textBoxMain_SelectionChanged);
            this.ScrollIn = new RelayCommand<object>(this.scrollIn);
            this.ScrollOut = new RelayCommand<object>(this.scrollOut);
            this.UndoCommand = new RelayCommand<object>(this.undoOperation);
            this.RedoCommand = new RelayCommand<object>(this.redoOperation);
            

            fileName = "";
            focusedObj = (MainWindow)System.Windows.Application.Current.MainWindow;
            rtbCount = 0;
            minWidth = 0;
            fontSizeIndex = 1;
            isBoldChecked = false;
            isItalicChecked = false;
            visibility = Visibility.Collapsed;
            zoomValue = 1;

        }
        #region PropertyFields
   
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

        private void scrollIn(object sender)
        {
            if(ZoomValue < 10)
            {
                ZoomValue = ZoomValue + 0.5;

            }
            
        }

        private void scrollOut(object sender)
        {
            if (ZoomValue > 1)
            {
                ZoomValue = ZoomValue - 0.5;

            }

        }

        #endregion

        #region Generic calls

        public void undoOperation(object sender)
        {
            undoRedoController.Undo();
        }

        public void redoOperation(object sender)
        {
            undoRedoController.Redo();
        }

        

        #endregion

        #region Menu Item calls

        private void createNewFractionControl()
        {
            addFormula(new FractionModel());
        }

        private void createNewPowControl()
        {
            addFormula(new PowModel());
        }

        private void createNewSqrtControl()
        {
            addFormula(new SquareModel());
        }

        private void addFormula(Operation formula)
        {
            undoRedoController.AddAndExecute(new AddFormulaCommand(formulas, formula));
        }

        private void changeFontSize(object sender)
        {
            parentTb = (RichTextBox)FocusManager.GetFocusedElement(focusedObj);
            TextSelection text = parentTb.Selection;
            parentTb.Focus();
            double size;
            if (Double.TryParse(fontSize, out size))
            {
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
        public void openDoc(object sender)
        {
            formulas = new ObservableCollection<Operation>();
            DocumentHelper helper = new DocumentHelper();
            ObservableCollection<Operation> tempformulas = helper.openFile();
            foreach(Operation o in tempformulas)
            {
                addFormula(o);
            }
        }

        private void saveDoc(object sender)
        {
            SerializeObjectToXML(formulas, false);
        }

        private void saveAsDoc(object sender)
        {
            SerializeObjectToXML(formulas, true);
        }

        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        private void SerializeObjectToXML<T>(T item , bool isSaveAsCaller)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                var xmlSerializer = new XmlSerializer(item.GetType());
                var stringBuilder = new StringBuilder();
                var xmlTextWriter = XmlTextWriter.Create(stringBuilder, new XmlWriterSettings { NewLineChars = "\r\n", Indent = true });
                xmlSerializer.Serialize(xmlTextWriter, item);
                var finalXml = stringBuilder.ToString();
                BinaryFlowDocument = Encoding.ASCII.GetBytes(finalXml);
            }

            if (isSaveAsCaller)
            {
                var command = new AsyncRelayCommand<object>(saveAsAsync, (a) => { return !this.isSaving; });
                command.Execute(BinaryFlowDocument);
            }
            else
            {
                var command = new AsyncRelayCommand<object>(saveAsync, (a) => { return !this.isSaving; });
                command.Execute(BinaryFlowDocument);
            }
        }

        public static void SerializeObjectToXML<T>(T item, string FilePath)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            using (StreamWriter wr = new StreamWriter(FilePath))
            {
                xs.Serialize(wr, item);
            }
        }

        private void saveAsync(object sender)
        {
            DocumentHelper helper = new DocumentHelper();
            helper.saveDoc(BinaryFlowDocument, fileName);
        }

        private void saveAsAsync(object sender)
        {
            DocumentHelper helper = new DocumentHelper();
            helper.saveAsDoc(BinaryFlowDocument);
        }
        #endregion


    }
}
