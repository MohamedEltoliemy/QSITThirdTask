using Autodesk.Revit.UI;
using System.IO;
using System.Reflection;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace QSITThirdTask
{
    public class QSITExternalApplication : IExternalApplication
    {
        #region properties
        public static Assembly ThisAssembly { get; } = Assembly.GetExecutingAssembly();


        #endregion

        #region interface methods

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            string tabName = "QSIT";
            application.CreateRibbonTab(tabName);
            var panel = application.CreateRibbonPanel(tabName, "Delete Elements");

            CreateAddinButton(panel, "Delete", "Delete the picked element or you can select all windows and delete them", "qsit_logo.png", "QSITThirdTask.QSITExternalCommand");
            return Result.Succeeded;
        }


        #endregion

        #region Helper Methods
        private void CreateAddinButton(RibbonPanel panel, string buttonName, string toolTip, string image_Name, string className)
        {


            string assemblyName = ThisAssembly.GetName().Name;

            //Add-in Button:
            PushButtonData pbData_ExtCmd = new PushButtonData($"{buttonName}_btn", buttonName, ThisAssembly.Location, $"{className}");
            PushButton pb_ExtCmd = panel.AddItem(pbData_ExtCmd) as PushButton;
            pb_ExtCmd.ToolTip = toolTip;

            // Button Image:
            string image_EmbeddedPath = $"{assemblyName}.Resources.{image_Name}";
            pb_ExtCmd.LargeImage = getImageSource(image_EmbeddedPath);
            //pb_ExtCmd.LargeImage = new BitmapImage(new Uri($"pack://application:,,,/Formwork;component/Resources/{image_Name}"));

        }

        private ImageSource getImageSource(string imageEmbeddedPath)
        {
            BitmapDecoder decoder = null;
            string image_Extension = Path.GetExtension(imageEmbeddedPath);
            Stream stream = ThisAssembly.GetManifestResourceStream(imageEmbeddedPath);  // Returns null if the 'Build Action' of the image isn't 'Embedded Resource'.

            switch (image_Extension)
            {
                case ".png":
                    decoder = new PngBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                    break;

                case ".bmp":
                    decoder = new BmpBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                    break;

                case ".jpg":
                case ".jpeg":
                    decoder = new JpegBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                    break;

                case ".ico":
                    decoder = new IconBitmapDecoder(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                    break;
            }

            return decoder?.Frames[0];
        }

        #endregion
    }
}
