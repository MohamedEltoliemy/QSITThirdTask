using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;

namespace QSITThirdTask.EventHandler
{
    public class DeleteProjectWindowsHandler : IExternalEventHandler
    {
        public UIApplication UIApplication { get; set; }

        public void Execute(UIApplication app)
        {
            UIApplication = app;
            UIDocument uiDocument = UIApplication.ActiveUIDocument;
            Document document = uiDocument.Document;

            try
            {
                var selection = uiDocument.Selection;
                var pickrefs = selection.PickObjects(ObjectType.Element, new CategorySelectionFilter(BuiltInCategory.OST_Windows), "Select windows to delete");

                if (pickrefs == null || pickrefs.Count == 0)
                {
                    TaskDialog.Show("Warning", "No windows selected.");
                    return;
                }

                var windowIdsToDelete = new List<ElementId>();

                foreach (var pickref in pickrefs)
                {
                    var window = document.GetElement(pickref);
                    if (window != null && window.Category != null && window.Category.Id.IntegerValue == (int)BuiltInCategory.OST_Windows)
                    {
                        windowIdsToDelete.Add(window.Id);
                    }
                }

                if (windowIdsToDelete.Count == 0)
                {
                    TaskDialog.Show("Warning", "No valid windows selected.");
                    return;
                }

                using (Transaction transaction = new Transaction(document, "Delete Selected Windows"))
                {
                    transaction.Start();
                    foreach (var id in windowIdsToDelete)
                    {
                        document.Delete(id);
                    }
                    transaction.Commit();
                }

                TaskDialog.Show("Success", "Selected windows deleted successfully.");
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

        public string GetName() => "Delete Project Windows Handler";
    }
}



