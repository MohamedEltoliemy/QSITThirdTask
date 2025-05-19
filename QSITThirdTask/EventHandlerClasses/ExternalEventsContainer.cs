using Autodesk.Revit.UI;

namespace QSITThirdTask.EventHandler
{
    public static class ExternalEventsContainer
    {
        public static DeleteSelectedElementHandler DeleteSelectedElementHandler { get; private set; }
        public static ExternalEvent DeleteElementEvent { get; private set; }
        public static DeleteProjectWindowsHandler DeleteProjectWindowsHandler { get; private set; }

        public static ExternalEvent DeleteProjectWindowsEvent { get; private set; }

        static ExternalEventsContainer()
        {
            DeleteSelectedElementHandler = new DeleteSelectedElementHandler();
            DeleteElementEvent = ExternalEvent.Create(DeleteSelectedElementHandler);
            DeleteProjectWindowsHandler = new DeleteProjectWindowsHandler();
            DeleteProjectWindowsEvent = ExternalEvent.Create(DeleteProjectWindowsHandler);
        }
    }
}
