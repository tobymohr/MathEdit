﻿using MathEdit.Command;
using MathEdit.Helpers;
using MathEdit.Model;
using MathEdit.Views;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Threading;
using System.Xml;
using System.Xml.Serialization;

namespace MathEdit.ViewModels
{
    class MainWindowModel : ViewModelBase
    {
        public ICommand SaveCommand { get; }
        public ICommand CreateFractionCommand { get; }
        public ICommand CreatePowCommand { get; }
        public ICommand CreateSqrtCommand { get; }
        public ICommand OpenCommand { get; }
        public ICommand SaveAsCommand { get; }
        public ICommand OpenHotkeysCommand { get; }
        public ICommand OpenSettingsCommand { get; }
        public ICommand CreateNewRTBCommand { get; }
        public ICommand NewCommand { get; }

        public ICommand ToggleBold { get; }
        public ICommand ToggleItalic { get; }
        public ICommand ChangeFontSize { get; }
        public ICommand TextBoxMainSelectionChanged { get; }
        public ICommand ScrollIn { get; }
        public ICommand ScrollOut { get; }

        public ICommand UndoCommand { get; }
        public ICommand RedoCommand { get; }

        public FlowDocumentModel documentModel;
        public EnabledFlowDocument mainFlowDocument;
        public BlockUIContainer bU;

        public HotkeyMenu hotKeys { get; set; }
        public bool isSaving { get; set; }
        public RichTextBox parentTb { get; set; }
        public int rtbCount { get; set; }
        public double minWidth { get; set; }
        public MainWindow focusedObj;
        private string fileName;
        private string fontSize;
        private int fontSizeIndex;
        private bool isBoldChecked;
        private Operation latestOperation;
        private bool isItalicChecked;
        private Visibility visibility;
        private double zoomValue;
        private UndoRedo undoRedo;

        public MainWindowModel()
        {
            this.SaveCommand = new RelayCommand<object>(this.saveDoc); // Async IO declaration in method
            this.OpenCommand = new RelayCommand<object>(this.openDoc);
            this.SaveAsCommand = new RelayCommand<object>(this.saveAsDoc); // Async IO declaration in method
            this.OpenHotkeysCommand = new RelayCommand<object>(this.openHotKeys);
            this.OpenSettingsCommand = new RelayCommand<object>(this.openSettings);
            this.ToggleBold = new RelayCommand<object>(this.bold_Click);
            this.ToggleItalic = new RelayCommand<object>(this.italic_Click);
            this.ChangeFontSize = new RelayCommand<object>(this.changeFontSize);
            this.CreateFractionCommand = new RelayCommand<object>(this.createNewFractionControl);
            this.CreatePowCommand = new RelayCommand<object>(this.createNewPowControl);
            this.CreateSqrtCommand = new RelayCommand<object>(this.createNewSqrtControl);
            this.TextBoxMainSelectionChanged = new RelayCommand<object>(this.textBoxMain_SelectionChanged);
            this.ScrollIn = new RelayCommand<object>(this.scrollIn);
            this.ScrollOut = new RelayCommand<object>(this.scrollOut);
            this.UndoCommand = new RelayCommand<object>(this.undoOperation);
            this.RedoCommand = new RelayCommand<object>(this.redoOperation);
            this.NewCommand = new RelayCommand<object>(this.newDocument);

            bU = new BlockUIContainer();
            documentModel = new FlowDocumentModel();
            mainFlowDocument = documentModel.mainFlowDocument;
            focusedObj = (MainWindow)System.Windows.Application.Current.MainWindow;
            fileName = "";
            rtbCount = 0;
            minWidth = 0;
            fontSizeIndex = 2;
            isBoldChecked = false;
            isItalicChecked = false;
            visibility = Visibility.Collapsed;
            zoomValue = 1;
            undoRedo = new UndoRedo();
        }
        #region PropertyFields
        public EnabledFlowDocument MainFlowDocument
        {
            get { return documentModel.mainFlowDocument; }
            set { this.SetProperty(ref mainFlowDocument, value); }
        }

        public byte[] FlowDocumentBytes
        {
            get { return documentModel.flowDocumentBytes; }
            set { documentModel.flowDocumentBytes = value; }
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

        private void newDocument(object sender)
        {
            MainFlowDocument.Blocks.Clear();
            MainFlowDocument.childrenOperations.Clear();
        }

        private void scrollIn(object sender)
        {
            if (ZoomValue < 10)
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
            //undoRedoController.Undo();
            UndoRedoObject uro = undoRedo.Undo();
            if (uro == null) return;
            deleteOrRecreate(uro);
            uro.Deleted = !uro.Deleted;
        }

        public void redoOperation(object sender)
        {
            //undoRedoController.Redo();
            UndoRedoObject uro = undoRedo.Redo();
            if (uro == null) return;
            deleteOrRecreate(uro);
            uro.Deleted = !uro.Deleted;
        }

        private void addFormula(UserControl uc)
        {
            //undoRedoController.AddAndExecute(new AddFormulaCommand(formulas, latestOperation));
            //False means it is not being delete, therefore being created.
            undoRedo.Add(new UndoRedoObject(uc,false));
        }

        private void deleteOrRecreate(UndoRedoObject uro)
        {
            UserControl tempUserControl = uro.Uc;
            MathControl controlModel = tempUserControl as MathControl;
            if (uro.Deleted)
            {
                //Tilføjer barnets model
                controlModel.model.getParent.childrenOperations.Add(controlModel.model);
                if (uro.TextPointer == null)
                {
                    //Hvis ingen textPointer er sat, så benyt modellens position
                    Paragraph p =
                        controlModel.model.getParent.Blocks.ElementAt(controlModel.model.blockPosition) as Paragraph;
                    InlineUIContainer container = new InlineUIContainer { Child = tempUserControl };
                    container.Unloaded += UserControl_Unloaded;
                    int pos = controlModel.model.parPosition;
                    if (p.Inlines.Count == 0 || pos>=p.Inlines.Count)
                    {
                        p.Inlines.Add(container);
                    }
                    else
                    {
                        p.Inlines.InsertBefore(p.Inlines.ElementAt(pos), container);
                    }
                }
                else
                {
                    //Hvis textPointer er sat, så er modellen position ikke, derfor brug textPointer
                    InlineUIContainer container = new InlineUIContainer(tempUserControl, uro.TextPointer);
                   container.Unloaded += UserControl_Unloaded;
                }
            }
            else
            {
                //    //Fjerner child fra EnabledFlowDocument
                    controlModel.model.getParent.childrenOperations.Remove(controlModel.model);
                    //Fjerner det fra UI
                    int blockPosition = 0;
                    foreach (Block b in controlModel.model.getParent.Blocks)
                    {
                        var inlinePosition = 0;
                        if (b is Paragraph)
                        {
                            Paragraph p = (Paragraph)b;
                            foreach (Inline inline in p.Inlines)
                            {
                                if (inline is InlineUIContainer)
                                {
                                    InlineUIContainer uic = (InlineUIContainer)inline;
                                    if (uic.Child == tempUserControl)
                                    {
                                        controlModel.model.parPosition = inlinePosition;
                                        controlModel.model.blockPosition = blockPosition;
                                        inline.Unloaded -= UserControl_Unloaded;
                                        uic.Child = null;
                                        p.Inlines.Remove(inline);
                                        return;
                                    }
                                }
                                inlinePosition++;
                            }
                        }
                        blockPosition++;
                    }
            }
        }

        #endregion

        #region Menu Item calls

        private void setupDoc(RichTextBox parentTb, UIElement element, Operation model)
        {
            EnabledFlowDocument parentFd;

            if (parentTb.Document.GetType() == typeof(EnabledFlowDocument))
            {
                parentFd = (EnabledFlowDocument)parentTb.Document;
            }
            else
            {
                parentFd = new EnabledFlowDocument("");
                parentFd.Blocks.Add(bU);
                parentTb.Document = parentFd;
            }

            InlineUIContainer container = new InlineUIContainer(element, parentTb.CaretPosition);
            container.Unloaded += UserControl_Unloaded;
            parentFd.childrenOperations.Add(model);
        }

    

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            InlineUIContainer container = sender as InlineUIContainer;
            if(container.Child != null) { 
                MathControl ctrlmodel = container.Child as MathControl;
                Operation localmodel = ctrlmodel.model;
                //Fjerner child fra parent og sætter position
                localmodel.getParent.childrenOperations.Remove(localmodel);
                //Finder position
                IInputElement focusedControl = FocusManager.GetFocusedElement(focusedObj);
                RichTextBox parentBox = focusedControl as RichTextBox;
                UndoRedoObject uro = new UndoRedoObject((UserControl) container.Child, true, parentBox.CaretPosition);
                undoRedo.Add(uro);
                container.Child = null;
            }
        }

        private void createNewFractionControl(object sender)
        {
            IInputElement focusedControl = FocusManager.GetFocusedElement(focusedObj);
            RichTextBox parentBox = focusedControl as RichTextBox;
            EnabledFlowDocument parentFd = parentBox.Document as EnabledFlowDocument;
            FractionControl fControl = new FractionControl(parentFd);
            MathControl icontrol = fControl as MathControl;
            setupDoc(parentBox, fControl, icontrol.model);
            startEnabledFlowDoc(parentFd);
            addFormula(fControl);
        }


        private void createNewPowControl(object sender)
        {
            IInputElement focusedControl = FocusManager.GetFocusedElement(focusedObj);
            RichTextBox parentBox = focusedControl as RichTextBox;
            EnabledFlowDocument parentFd = parentBox.Document as EnabledFlowDocument;
            PowControl pControl = new PowControl(parentFd);
            MathControl icontrol = pControl as MathControl;
            setupDoc(parentBox, pControl, icontrol.model);
            startEnabledFlowDoc(parentFd);
            addFormula(pControl);
        }

        private void createNewSqrtControl(object sender)
        {
            IInputElement focusedControl = FocusManager.GetFocusedElement(focusedObj);
            RichTextBox parentBox = focusedControl as RichTextBox;
            EnabledFlowDocument parentFd = parentBox.Document as EnabledFlowDocument;
            SquareControl sControl = new SquareControl(parentFd);
            MathControl icontrol = sControl as MathControl;
            setupDoc(parentBox, sControl, icontrol.model);
            startEnabledFlowDoc(parentFd);
            addFormula(sControl);
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


        private void startEnabledFlowDoc(EnabledFlowDocument doc)
        {
            Paragraph p = doc.Blocks.LastBlock as Paragraph;
            Run run = new Run(" ");
            p.Inlines.Add(run);
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
            // needs work
            DocumentHelper helper = new DocumentHelper();
            EnabledFlowDocument newDoc = null;
            using (MemoryStream stream = new MemoryStream())
            {
                newDoc = helper.openFile();
            }
            if(newDoc != null)
            {
                newDocument(this);
                openDocInGUI(MainFlowDocument, newDoc);
            }
        }

        private void openDocInGUI(EnabledFlowDocument currentDocument, EnabledFlowDocument loadDoc)
        {
            Paragraph par = null;
            if(loadDoc.text != " " && loadDoc.text != "")
            {
                Run run = new Run(loadDoc.text);
                par = new Paragraph();
                currentDocument.text = loadDoc.text;
                par.Inlines.Add(run);
                currentDocument.Blocks.Add(par);
            }
            
            int prevBlockPos = -1;
            foreach (Operation op in loadDoc.childrenOperations.ToList<Operation>())
            {
                UIElement element = GetUIElementForType(op, currentDocument);
                if (prevBlockPos != op.blockPosition)
                {
                    prevBlockPos = op.blockPosition;
                    par = new Paragraph();
                    InlineUIContainer container = new InlineUIContainer();
                    container.Child = element;
                    container.Unloaded += UserControl_Unloaded;
                    par.Inlines.Add(container);
                    currentDocument.Blocks.Add(par);
                }else
                {
                    par = (Paragraph) currentDocument.Blocks.LastBlock;
                    par.Inlines.Add(element);
                }
            }
        }

        private void setupChildDocs(Operation childModel, Operation loadModel, EnabledFlowDocument currentDocument)
        {
            for(int i = 0; i < loadModel.ListOfEnabledDocs.Count; i++)
            {
                openDocInGUI(childModel.ListOfEnabledDocs.ElementAt(i), loadModel.ListOfEnabledDocs.ElementAt(i));
            }
        }

        private UIElement GetUIElementForType(Operation op ,EnabledFlowDocument currentDocument)
        {
            UserControl uControl = null;
            if(op.GetType() == typeof(FractionModel))
            {
                uControl = new FractionControl(currentDocument);
            }else if (op.GetType() == typeof(SquareModel))
            {
                uControl = new SquareControl(currentDocument);
            } else if (op.GetType() == typeof(PowModel))
            {
                uControl = new PowControl(currentDocument);
            }
           
            MathControl controlModel = uControl as MathControl;
            setupChildDocs(controlModel.model, op, currentDocument);
            currentDocument.childrenOperations.Add(controlModel.model);
            return uControl;
        }

        private void saveDoc(object sender)
        {
            MainFlowDocument = sender as EnabledFlowDocument;
            DocumentHelper helper = new DocumentHelper();
            string dialogResult = null;

            serializeDocument(); // Serialize model
            // Get dialog if first save.
            if (fileName == "")
            {
                dialogResult = helper.getSaveDialog();
                if (dialogResult != null)
                {
                    fileName = dialogResult;
                }
            }

            // Check if FileName is set, if not - cancel
            if (fileName != "")
            {
                var saveExecute = new AsyncRelayCommand<object>(saveAsync, (a) => { return !this.isSaving; });
                if (this.isSaving == false)
                    saveExecute.Execute(null);
                else
                    showErrorMessage("Something went wrong while saving, try again", "Save Error");
            }
        }

   

        private void saveAsDoc(object sender)
        {
            MainFlowDocument = sender as EnabledFlowDocument;
            DocumentHelper helper = new DocumentHelper();

            string dialogResult = null;

            serializeDocument(); // Serialize model
            dialogResult = helper.getSaveDialog(); // Get dialog

            // Save if user did not cancel
            if (dialogResult != null)
            {
                fileName = dialogResult;
                var saveExecute = new AsyncRelayCommand<object>(saveAsync, (a) => { return !this.isSaving; });
                if(this.isSaving == false)
                    saveExecute.Execute(null);
                else
                    showErrorMessage("Something went wrong while saving, try again", "Save Error");
            }
        }

        private void serializeDocument()
        {
            String finalXml;
            var utf8NoBom = new UTF8Encoding(false);
            setPositions(MainFlowDocument);
            ListOfEnabledDocs docs = new ListOfEnabledDocs { MainFlowDocument };
            var xmlSerializer = new XmlSerializer(docs.GetType());
            var stringBuilder = new StringBuilder();
            var xmlTextWriter = XmlTextWriter.Create(stringBuilder, new XmlWriterSettings { Indent = true, Encoding = utf8NoBom });
            xmlSerializer.Serialize(xmlTextWriter, docs);
            finalXml = stringBuilder.ToString();
            finalXml = finalXml.Replace("&#xA", "");
            finalXml = finalXml.Replace("&#xD;;", "");
            FlowDocumentBytes = Encoding.ASCII.GetBytes(finalXml);     
        }

        private void saveAsync(object sender)
        {
            DocumentHelper helper = new DocumentHelper();
            helper.saveDoc(FlowDocumentBytes, fileName);
        }

        private void setPositions(EnabledFlowDocument document)
        {
            int blockCounter = 0;
            int parCounter = 0;
            foreach (Block b in document.Blocks)
            {
                if (b is Paragraph)
                {
                    parCounter = 0;
                    foreach (Inline inline in ((Paragraph)b).Inlines)
                    {
                        if (inline is InlineUIContainer)
                        {
                            Operation model = null;
                            InlineUIContainer container = (InlineUIContainer)inline;
                            MathControl contrl = container.Child as MathControl;
                        
                            if (model != null)
                            {
                                model = contrl.model;
                                model.blockPosition = blockCounter;
                                model.parPosition = parCounter;
                                foreach (EnabledFlowDocument tempDoc in model.ListOfEnabledDocs)
                                {
                                    setPositions(tempDoc);
                                }
                            }
                           
                        }
                        else if (inline is Run)
                        {
                            Console.WriteLine("RUN");
                        }
                        parCounter++;
                    }
                }
                blockCounter++;
            }
        }

        private void showErrorMessage(string text, string caption)
        {
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBox.Show(text, caption, button, icon);
        }
        #endregion
    }
}
