using MathEdit.Command;
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
        public BlockUIContainer bU = new BlockUIContainer();

        public string fileName { get; set; }
        public HotkeyMenu hotKeys { get; set; }
        public bool isSaving { get; set; }
        public RichTextBox parentTb { get; set; }
        public int rtbCount { get; set; }
        public double minWidth { get; set; }
        public MainWindow focusedObj;
        private string fontSize;
        private int fontSizeIndex;
        private bool isBoldChecked;
        private Operation latestOperation;
        private bool isItalicChecked;
        private bool dropDownOpen;
        private Visibility visibility;
        private double zoomValue;
        private UndoRedo undoRedo;


        public MainWindowModel()
        {
            this.SaveCommand = new RelayCommand<object>(this.saveDoc);
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
            this.ScrollIn = new RelayCommand<object>(this.scrollIn);
            this.ScrollOut = new RelayCommand<object>(this.scrollOut);
            this.UndoCommand = new RelayCommand<object>(this.undoOperation);
            this.RedoCommand = new RelayCommand<object>(this.redoOperation);
            this.NewCommand = new RelayCommand<object>(this.newDocument);

            documentModel = new FlowDocumentModel();
            mainFlowDocument = documentModel.mainFlowDocument;

            fileName = "";
            focusedObj = (MainWindow)System.Windows.Application.Current.MainWindow;
            rtbCount = 0;
            minWidth = 0;
            fontSizeIndex = 1;
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

        public byte[] BinaryFlowDocument
        {
            get { return documentModel.binaryFlowDocument; }
            set { documentModel.binaryFlowDocument = value; }
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

        private void addFormula(UserControl uc, string type)
        {
            //undoRedoController.AddAndExecute(new AddFormulaCommand(formulas, latestOperation));
            //False means it is not being delete, therefore added.
            undoRedo.Add(uc,false);
        }

        private void deleteOrRecreate(UndoRedoObject uro)
        {
            if (uro.Deleted)
            {
                //recreate
                if (uro.Uc.GetType() == typeof(FractionControl))
                {
                    FractionControl fControl = (FractionControl)uro.Uc;
                    fControl.model.getParent().childrenOperations.Add(fControl.model);
                    //par.Inlines.Add(fControl);
                    Paragraph p = fControl.model.getParent().Blocks.ElementAt(fControl.model.blockPosition) as Paragraph;
                    //p.Inlines.ElementAt(fControl.)
                    //Mangler bestemt position
                }
                else if (uro.Uc.GetType() == typeof(PowControl))
                {

                }
            }
            else
            {
                //delete
                if (uro.Uc.GetType() == typeof(FractionControl))
                {
                    //Fjerner child fra EnabledFlowDocument
                    FractionControl fControl = (FractionControl)uro.Uc;
                    fControl.model.getParent().childrenOperations.Remove(fControl.model);
                    //Fjerner det fra UI
                    int blockPosition = 0;
                    int inlinePosition = 0;
                    foreach (Block b in fControl.model.getParent().Blocks)
                    {
                        if (b is Paragraph)
                        {
                            Paragraph p = (Paragraph) b;
                            foreach (Inline inline in p.Inlines)
                            {
                                if (inline is InlineUIContainer)
                                {
                                    InlineUIContainer uic = (InlineUIContainer) inline;
                                    if (uic.Child == fControl)
                                    {
                                        fControl.model.parPosition = inlinePosition;
                                        fControl.model.blockPosition = blockPosition;
                                        uic.Child = null;
                                        return;
                                    }
                                }
                                inlinePosition++;
                            }
                        }
                        blockPosition++;
                    }

                }else if (uro.Uc.GetType() == typeof(PowControl))
                {
                    
                }
                
            }
        }

        #endregion

        #region Menu Item calls

        private void setupDoc(RichTextBox parentTb)
        {
            FlowDocument parentFd;
          
            if (parentTb.Document.GetType() == typeof(EnabledFlowDocument))
            {
                parentFd = parentTb.Document;
            }
            else
            {
                parentFd = new EnabledFlowDocument("");
                parentFd.Blocks.Add(bU);
                parentTb.Document = parentFd;
            }
        }

        private Paragraph insertOnParagraph(RichTextBox parentTb, TextPointer position)
        {
            EnabledFlowDocument parentFd = (EnabledFlowDocument) parentTb.Document;
            Paragraph para = null;
            
            if (position.Parent.GetType() != typeof(Paragraph) && position.Parent.GetType() != typeof(Run))
            {
                para = new Paragraph();
                parentFd.Blocks.Add(para);
               
            }else
            {
                if(position.Parent.GetType() == typeof(Run))
                {
                    Run run = (Run)position.Parent;
                    Console.WriteLine(run.Parent.GetType());
                    para = (Paragraph)run.Parent;
                }
                else
                {
                    para = (Paragraph)position.Parent;
                }
            }
            return para;
        }

        void SetIntPosition(int intPosition, RichTextBox rtb)
        {
            TextPointer currentPosition = rtb.Document.ContentStart;
            for (int i = 0; i < intPosition; i++)
            {
                currentPosition = currentPosition.GetNextInsertionPosition(LogicalDirection.Forward);
            }

            if(currentPosition == null)
            {
                currentPosition = rtb.Document.ContentStart;
            }
            rtb.CaretPosition = currentPosition;
        }

        int GetIntPosition(TextPointer pointerPosition, RichTextBox rtb)
        {
            int intPosition = 0;
            TextPointer currentPosition = rtb.Document.ContentStart;
                while (currentPosition.CompareTo(pointerPosition) != 0)
                {
                    intPosition++;
                    currentPosition = currentPosition.GetNextInsertionPosition(LogicalDirection.Forward);
                    if(currentPosition == null)
                    {
                        break;
                    }
            }
            return intPosition;
        }

        private void createNewFractionControl()
        {
            Paragraph para = null;
            IInputElement focusedControl = FocusManager.GetFocusedElement(focusedObj);
            RichTextBox parentBox = focusedControl as RichTextBox;
            setupDoc(parentBox);
            TextPointer position = parentBox.CaretPosition;
            para = insertOnParagraph(parentBox, position);
            EnabledFlowDocument parentFd = parentBox.Document as EnabledFlowDocument;
            FractionControl fControl = new FractionControl(parentFd);
            InlineUIContainer container = new InlineUIContainer();
            container.Child = fControl;
            container.Unloaded += presenter_Unloaded;
            parentFd.childrenOperations.Add(fControl.model);
            para.Inlines.Add(container);
            
            //latestOperation = fControl.model;
            addFormula(fControl,"fraction");
        }

        void presenter_Unloaded(object sender, RoutedEventArgs e)
        {
            InlineUIContainer container = sender as InlineUIContainer;
            Operation model = null;
            if (container.Child is FractionControl)
            {
                FractionControl f = (FractionControl)container.Child;
                model = f.model;

            }
            else if (container.Child is SquareControl)
            {
                SquareControl f = (SquareControl)container.Child;
                model = f.model;
            }
            else if (container.Child is PowControl)
            {
                PowControl f = (PowControl)container.Child;
                model = f.model;
            }
            Console.WriteLine(container.Child.GetType());
            Console.WriteLine(model.ListOfEnabledDocs.ElementAt(0).text);

        }

        private void createNewPowControl()
        {
            Paragraph para = null;
            IInputElement focusedControl = FocusManager.GetFocusedElement(focusedObj);
            RichTextBox parentBox = focusedControl as RichTextBox;
            setupDoc(parentBox);
            TextPointer position = parentBox.CaretPosition;
            para = insertOnParagraph(parentBox, position);
            EnabledFlowDocument parentFd = parentBox.Document as EnabledFlowDocument;
            PowControl pControl = new PowControl(parentFd);
            InlineUIContainer container = new InlineUIContainer();
            container.Child = pControl;
            container.Unloaded += presenter_Unloaded;
            parentFd.childrenOperations.Add(pControl.model);
            para.Inlines.Add(container);
        }

        private void createNewSqrtControl()
        {
            Paragraph para = null;
            IInputElement focusedControl = FocusManager.GetFocusedElement(focusedObj);
            RichTextBox parentBox = focusedControl as RichTextBox;
            setupDoc(parentBox);
            TextPointer position = parentBox.CaretPosition;
            para = insertOnParagraph(parentBox, position);
            EnabledFlowDocument parentFd = parentBox.Document as EnabledFlowDocument;
            SquareControl sControl = new SquareControl(parentFd);
            InlineUIContainer container = new InlineUIContainer();
            container.Child = sControl;
            container.Unloaded += presenter_Unloaded;
            parentFd.childrenOperations.Add(sControl.model);
            para.Inlines.Add(container);
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
            if(loadDoc.text != "")
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
                Console.WriteLine(op.GetType());
               
                if (prevBlockPos != op.blockPosition)
                {
                    prevBlockPos = op.blockPosition;
                    par = new Paragraph();
                    par.Inlines.Add(element);
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
            if(op.GetType() == typeof(FractionModel))
            {
                FractionControl f = new FractionControl(currentDocument);
                setupChildDocs(f.model, op, currentDocument);
                currentDocument.childrenOperations.Add(f.model);
                return f;
            }else if (op.GetType() == typeof(SquareModel))
            {
                SquareControl f = new SquareControl(currentDocument);
                setupChildDocs(f.model, op, currentDocument);
                currentDocument.childrenOperations.Add(f.model);
                return f;
            } else if (op.GetType() == typeof(PowModel))
            {
                PowControl f = new PowControl(currentDocument);
                setupChildDocs(f.model, op, currentDocument);
                currentDocument.childrenOperations.Add(f.model);
                return f;
            }
            else
            {
                return null;
            }
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
            }

            // Check if FileName is set, if not - cancel
            if (dialogResult != null || fileName != "")
            {
                fileName = dialogResult;
                // Queue for execution, await serialization
                var saveExecute = new AsyncRelayCommand<object>(saveAsync, (a) => { return !this.isSaving; });
                saveExecute.Execute(null);
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
                // Queue for execution, await serialization
                var saveExecute = new AsyncRelayCommand<object>(saveAsync, (a) => { return !this.isSaving; });
                saveExecute.Execute(null);
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
            BinaryFlowDocument = Encoding.ASCII.GetBytes(finalXml);     
        }

        private void saveAsync(object sender)
        {

            DocumentHelper helper = new DocumentHelper();
            helper.saveDoc(BinaryFlowDocument, fileName);

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
                            if (container.Child is FractionControl)
                            {
                                FractionControl f = (FractionControl)container.Child;
                                model = f.model;

                            }
                            else if (container.Child is SquareControl)
                            {
                                SquareControl f = (SquareControl)container.Child;
                                model = f.model;
                            }
                            else if (container.Child is PowControl)
                            {
                                PowControl f = (PowControl)container.Child;
                                model = f.model;
                            }
                            model.blockPosition = blockCounter;
                            model.parPosition = parCounter;
                            foreach (EnabledFlowDocument tempDoc in model.ListOfEnabledDocs)
                            {
                                setPositions(tempDoc);
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

        #endregion


    }
}
