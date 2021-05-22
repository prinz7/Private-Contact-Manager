using System;
using System.Collections.Generic;
using System.Threading;
using DesktopUI;
using DesktopUI.DataClasses;

namespace PrivateContactManager
{
    public class Program
    {
        public static void Main(string[] _)
        {
            var thread = new Thread(MainLoop);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
        }

        private static void MainLoop()
        {
            var mainWindow = new MainWindow();
            var windowController = new WindowController(ref mainWindow);

            mainWindow.Show();

            mainWindow.Closed += (_, _) => mainWindow.Dispatcher.InvokeShutdown();

            AddTestValues(ref mainWindow);

            System.Windows.Threading.Dispatcher.Run();
        }

        [Obsolete]
        private static void AddTestValues(ref MainWindow window)
        {
            var contacts = new List<Contact>
            {
                new Contact{ Forename = "Max", Surname = "Mustermann", Organization = "Audi AG", BirthdayDay = 7, BirthdayMonth = 7, BirthdayYear = 1993, IsFirstNameBasis = true },
                new Contact{ Forename = "Sabine", Surname = "Mustermann", Organization = "VW AG", BirthdayDay = 8, BirthdayMonth = 7, BirthdayYear = 1993, IsFirstNameBasis = false },
                new Contact{ Forename = "Ralf", Surname = "Müller", Organization = "Muster GmbH", BirthdayDay = 9, BirthdayMonth = 7, BirthdayYear = 1983, IsFirstNameBasis = false },
                new Contact{ Forename = "John", Surname = "Wolf", Organization = "", BirthdayDay = 7, BirthdayMonth = 6, BirthdayYear = 1973, IsFirstNameBasis = false },
                new Contact{ Forename = "John", Surname = "Lord", Organization = "Deep Purple", BirthdayDay = 1, BirthdayMonth = 6, BirthdayYear = 1953, IsFirstNameBasis = true },
                new Contact{ Forename = "Jan", Surname = "Kissinger", Organization = "", BirthdayDay = 7, BirthdayMonth = 5, BirthdayYear = 1963, IsFirstNameBasis = true }
            };

            window.SetContacts(contacts);
        }
    }
}
