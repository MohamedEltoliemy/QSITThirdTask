using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Windows.Controls.Primitives;

namespace QSITThirdTask.ViewModel
{
    public class QsitViewModel : BaseViewModel
    {
        #region fields
        private Document _document;
        private UIApplication _uiApplication;
        #endregion

        #region ctor

        public QsitViewModel(Document doc, UIApplication uiApplication)
        {
            _uiApplication = uiApplication;
            _document = doc;

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
            // Logic to delete the selected element
            TaskDialog.Show("Delete Selected Element", "This command will delete the selected element.");
        }
        private void ExecuteDeleteProjectWindows()
        {
            // Logic to delete project windows
            TaskDialog.Show("Delete Project Windows", "This command will delete all project windows.");
        }
        #endregion


    }
}
