using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Collections.Generic;
using System;
using System.Windows.Controls.Primitives;
using System.Windows;

namespace QSITThirdTask.ViewModel
{
    public class QsitViewModel : BaseViewModel
    {
        #region fields
        private Document _document;
        private UIApplication _uiApplication;
        private Window _window;

        #endregion

        #region ctor

        public QsitViewModel(Document doc, UIApplication uiApplication, Window window)
        {
            _uiApplication = uiApplication;
            _document = doc;
            _window = window;

            InitializeCommands();
        }
        #endregion

        #region commands
        public RelayCommand DeleteSelectedElementCommand { get; private set; }
        public RelayCommand DeleteProjectWindowsCommand { get; private set; }
        #endregion

        #region methods
        private void InitializeCommands()
        {

            DeleteSelectedElementCommand = new RelayCommand(ExecuteDeleteSelectedElement);
            DeleteProjectWindowsCommand = new RelayCommand(ExecuteDeleteProjectWindows);
           

        }
        #endregion

        #region commands methods



        private void ExecuteDeleteSelectedElement()
        {
            try
            {
                _window.Close();
                var selection = _uiApplication.ActiveUIDocument.Selection;

                
                Reference pickedRef = selection.PickObject(
                    Autodesk.Revit.UI.Selection.ObjectType.Element,
                    "Select an element to delete."
                );

                if (pickedRef == null)
                {
                    TaskDialog.Show("Warning", "No element selected.");
                    return;
                }

                
                Element elem = _document.GetElement(pickedRef);

                if (elem == null)
                {
                    TaskDialog.Show("Error", "The selected element is invalid.");
                    return;
                }

                
                using (Transaction transaction = new Transaction(_document, "Delete Selected Element"))
                {
                    transaction.Start();
                    _document.Delete(elem.Id);
                    transaction.Commit();
                }

                TaskDialog.Show("Success", "The selected element has been deleted successfully!");
            }
            catch (Autodesk.Revit.Exceptions.OperationCanceledException)
            {
               
                TaskDialog.Show("Cancelled", "Operation cancelled by user.");
            }
            catch (System.Exception ex)
            {
                TaskDialog.Show("Error", ex.Message);
            }
        }


        private void ExecuteDeleteProjectWindows()
        {
            try
            {
                _window.Close();
                var selection = _uiApplication.ActiveUIDocument.Selection;

                
                IList<Reference> pickedRefs = selection.PickObjects(
                    Autodesk.Revit.UI.Selection.ObjectType.Element,
                    new CategorySelectionFilter(BuiltInCategory.OST_Windows), 
                    "Select area to delete windows inside."
                );

                if (pickedRefs == null || pickedRefs.Count == 0)
                {
                    TaskDialog.Show("Warning", "No elements selected.");
                    return;
                }

                List<ElementId> windowIdsToDelete = new List<ElementId>();

                
                foreach (var pickedRef in pickedRefs)
                {
                    Element elem = _document.GetElement(pickedRef);

                    
                    if (elem != null && elem.Category != null && elem.Category.Id.IntegerValue == (int)BuiltInCategory.OST_Windows)
                    {
                        windowIdsToDelete.Add(elem.Id);
                    }
                }

                if (windowIdsToDelete.Count == 0)
                {
                    TaskDialog.Show("Info", "No windows found in the selected area.");
                    return;
                }

                using (Transaction transaction = new Transaction(_document, "Delete Selected Area Windows"))
                {
                    transaction.Start();
                    foreach (var id in windowIdsToDelete)
                    {
                        _document.Delete(id);
                    }
                    transaction.Commit();
                }

                TaskDialog.Show("Success", $"{windowIdsToDelete.Count} windows deleted successfully!");
            }
            catch (Autodesk.Revit.Exceptions.OperationCanceledException)
            {
               
                TaskDialog.Show("Cancelled", "Operation cancelled by user.");
            }
            catch (Exception ex)
            {
                TaskDialog.Show("Error", ex.Message);
            }
        }

        #endregion


    }
}
