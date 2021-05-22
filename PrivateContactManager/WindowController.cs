using DesktopUI;

namespace PrivateContactManager
{
    public class WindowController
    {
        private MainWindow window;

        public WindowController(ref MainWindow mainWindow)
        {
            window = mainWindow;

            AddEventHandler();
        }

        private void AddEventHandler()
        {
            window.AddEventHandler(MainWindow.ButtonInstance.MAIN_WINDOW_ADD_CONTACT, window.ShowAddContactWindow);
            window.AddEventHandler(MainWindow.ButtonInstance.CONTACT_WINDOW_CANCEL, window.ShowMainWindow);
        }
    }
}
