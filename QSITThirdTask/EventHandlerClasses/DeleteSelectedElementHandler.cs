using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;

namespace QSITThirdTask.EventHandler
{
    public class DeleteSelectedElementHandler : IExternalEventHandler
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
                Reference pickedRef = selection.PickObject(
                    Autodesk.Revit.UI.Selection.ObjectType.Element,
                    "Select an element to delete");

                if (pickedRef != null)
                {
                    ElementId elementId = pickedRef.ElementId;
                    using (Transaction transaction = new Transaction(document, "Delete Selected Element"))
                    {
                        transaction.Start();
                        document.Delete(elementId);
                        transaction.Commit();
                    }
                }

            }
            catch (Exception ex)
            {
                TaskDialog.Show("Error", ex.Message);
            }
            
        }

        public string GetName()
        {
           return "Delete Selected Element Handler";
        }
    }

}
