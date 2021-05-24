using DesktopUI;
using System;
using System.Linq;

namespace PrivateContactManager
{
    public class WindowController
    {
        private MainWindow window;

        public WindowController(ref MainWindow mainWindow)
        {
            window = mainWindow;

            AddEventHandler();

            window.ShowMainWindow();
        }

        private void AddEventHandler()
        {
            window.AddEventHandler(MainWindow.ButtonInstance.MAIN_WINDOW_ADD_CONTACT, window.ShowAddContactWindow);
            window.AddEventHandler(MainWindow.ButtonInstance.CONTACT_WINDOW_CANCEL, window.ShowMainWindow);
            window.AddEventHandlerComboboxBirthdayChanged(ValidateBirthdaySelection);
        }

        private void ValidateBirthdaySelection()
        {
            window.GetSelectedBirthday(out var day, out var month, out var year);

            var availableDates = new DateTime(1900, 1, 1).Range(DateTime.Now);

            if (year != null)
            {
                availableDates = availableDates.Where(d => d.Year == year);
            }

            if (month != null)
            {
                availableDates = availableDates.Where(d => d.Month == month);
            }

            if (day != null)
            {
                availableDates = availableDates.Where(d => d.Day == day);
            }

            var dates = availableDates.ToList();

            if (year != null)
            {
                if(!dates.Any(date => date.Year == year))
                {
                    window.ResetSelectedBirthdayYear();
                    ValidateBirthdaySelection();
                    return;
                }
            }

            if (month != null)
            {
                if (!dates.Any(date => date.Month == month))
                {
                    window.ResetSelectedBirthdayMonth();
                    ValidateBirthdaySelection();
                    return;
                }
            }

            if (day != null)
            {
                if (!dates.Any(date => date.Day == day))
                {
                    window.ResetSelectedBirthdayDay();
                    ValidateBirthdaySelection();
                    return;
                }
            }

            if (year != null && month != null && day != null)
            {
                if (new DateTime(year.Value, month.Value, day.Value) > DateTime.Now)
                {
                    window.ResetSelectedBirthdayYear();
                    ValidateBirthdaySelection();
                    return;
                }
            }
        }
    }
}
