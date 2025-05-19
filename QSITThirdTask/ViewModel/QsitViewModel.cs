using Autodesk.Revit.UI;
using System.Windows;
using QSITThirdTask.EventHandler;

namespace QSITThirdTask.ViewModel
{
    public class QsitViewModel : BaseViewModel
    {
        #region fields
        private UIApplication _uiApplication;
        private Window _window;
        #endregion

        #region ctor
        public QsitViewModel(UIApplication uiApplication, Window window)
        {
            _uiApplication = uiApplication;
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

        #region command methods

        private void ExecuteDeleteSelectedElement()
        {
            ExternalEventsContainer.DeleteProjectWindowsHandler.UIApplication = _uiApplication;
            ExternalEventsContainer.DeleteElementEvent.Raise();
            _window?.Close();
        }

        private void ExecuteDeleteProjectWindows()
        {
            ExternalEventsContainer.DeleteProjectWindowsHandler.UIApplication = _uiApplication;
            ExternalEventsContainer.DeleteProjectWindowsEvent.Raise();
            _window?.Close();
        }

        #endregion
    }
}
