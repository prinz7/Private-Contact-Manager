using System;
using System.Threading;
using DesktopUI;

namespace PrivateContactManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread thread = new Thread(MainLoop);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
        }

        private static void MainLoop()
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();

            mainWindow.Closed += (sender2, e2) => mainWindow.Dispatcher.InvokeShutdown();

            System.Windows.Threading.Dispatcher.Run();
        }
    }
}
