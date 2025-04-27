using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using QSITThirdTask.View;
using QSITThirdTask.ViewModel;
using System;



namespace QSITThirdTask
{
    [Transaction(TransactionMode.Manual)]
    public class QSITExternalCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                UIApplication uiapp = commandData.Application;
                Document doc = uiapp.ActiveUIDocument.Document;
                // Create and show the view model
                QSITWindow mainWindow = new QSITWindow();
                QsitViewModel viewModel = new QsitViewModel(doc, uiapp,mainWindow);
                
                mainWindow.DataContext = viewModel;

                mainWindow.ShowDialog();

                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                TaskDialog.Show("Error", ex.Message);
                throw new NotImplementedException();
            }
        }
    }
}
        
    

