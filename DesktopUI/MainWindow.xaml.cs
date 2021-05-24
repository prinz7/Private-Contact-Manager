using DesktopUI.DataClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DesktopUI
{
    public partial class MainWindow : Window
    {
        public enum UILanguage
        {
            ENGLISH, GERMAN
        }

        public enum ButtonInstance
        {
            MAIN_WINDOW_ADD_CONTACT,
            CONTACT_WINDOW_CANCEL,
            CONTACT_WINDOW_DELETE_CONTACT,
            CONTACT_WINDOW_SAVE_CONTACT
        }

        private static readonly Dictionary<UILanguage, CultureInfo> CultureInfoByLanguage = new Dictionary<UILanguage, CultureInfo>
        {
            { UILanguage.ENGLISH, new CultureInfo("en-US") },
            { UILanguage.GERMAN,  new CultureInfo("de-DE") }
        };

        public static readonly Dictionary<UILanguage, string> LanguageNameByLanguage = new Dictionary<UILanguage, string>
        {
            { UILanguage.ENGLISH, "English" },
            { UILanguage.GERMAN,  "Deutsch" }
        };

        public MainWindow()
        {
            InitializeComponent();
            SetCulture(System.Threading.Thread.CurrentThread.CurrentCulture);
            AddEventhandler();
        }

        public void AddEventHandler(ButtonInstance buttonInstance, Action cb)
        {
            var buttonExists = GetButton(buttonInstance, out var button);
            if (!buttonExists)
            {
                return;
            }

            button.Click += (_, _) => { cb(); };
        }

        public void AddEventHandlerComboboxBirthdayChanged(Action cb)
        {
            comboBirthdayDay.SelectionChanged += (_, _) => { cb(); };
            comboBirthdayMonth.SelectionChanged += (_, _) => { cb(); };
            comboBirthdayYear.SelectionChanged += (_, _) => { cb(); };
        }

        public void SetContacts(List<Contact> contacts)
        {
            listViewContacts.ItemsSource = contacts;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(listViewContacts.ItemsSource);

            view.GroupDescriptions.Add(new PropertyGroupDescription("FirstCharacter"));
            view.SortDescriptions.Add(new SortDescription("FullName", ListSortDirection.Ascending));
        }

        public void ShowMainWindow()
        {
            gridMainWindow.Visibility = Visibility.Visible;
            gridEditContact.Visibility = Visibility.Collapsed;
        }

        public void ShowAddContactWindow()
        {
            gridMainWindow.Visibility = Visibility.Collapsed;
            gridEditContact.Visibility = Visibility.Visible;

            buttonDeleteContact.Visibility = Visibility.Collapsed;
        }

        public void ShowEditContactWindow(Contact editedContact)
        {
            gridMainWindow.Visibility = Visibility.Collapsed;
            gridEditContact.Visibility = Visibility.Visible;

            buttonDeleteContact.Visibility = Visibility.Visible;

            // TODO
        }

        public void GetSelectedBirthday (out int? day, out int? month, out int? year)
        {
            day = comboBirthdayDay.SelectedItem as int?;
            month = comboBirthdayMonth.SelectedItem as int?;
            year = comboBirthdayYear.SelectedItem as int?;
        }

        public void ResetSelectedBirthdayDay()
        {
            comboBirthdayDay.SelectedIndex = -1;
        }

        public void ResetSelectedBirthdayMonth()
        {
            comboBirthdayMonth.SelectedIndex = -1;
        }

        public void ResetSelectedBirthdayYear()
        {
            comboBirthdayYear.SelectedIndex = -1;
        }

        private bool GetButton(ButtonInstance buttonInstance, out Button button)
        {
            switch (buttonInstance)
            {
                case ButtonInstance.MAIN_WINDOW_ADD_CONTACT:
                    button = buttonAddContact;
                    return true;
                case ButtonInstance.CONTACT_WINDOW_CANCEL:
                    button = buttonCancel;
                    return true;
                case ButtonInstance.CONTACT_WINDOW_DELETE_CONTACT:
                    button = buttonDeleteContact;
                    return true;
                case ButtonInstance.CONTACT_WINDOW_SAVE_CONTACT:
                    button = buttonSaveContact;
                    return true;
                default:
                    button = null;
                    return false;
            }
        }

        private void AddEventhandler()
        {
            comboLanguage.SelectionChanged += ComboLanguage_SelectionChanged;
        }

        private void ComboLanguage_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var lang = e.AddedItems[0] as UILanguage? ?? UILanguage.ENGLISH;
            SetCulture(CultureInfoByLanguage[lang]);
        }

        private void SetCulture(CultureInfo culture)
        {
            var matchingCultureInfos = CultureInfoByLanguage.Where(kv => kv.Value.Name == culture.Name).ToList();
            if (!matchingCultureInfos.Any())
            {
                matchingCultureInfos = CultureInfoByLanguage.Where(kv => kv.Value.TwoLetterISOLanguageName == culture.TwoLetterISOLanguageName).ToList();
            }
            if (!matchingCultureInfos.Any())
            {
                matchingCultureInfos.AddRange(CultureInfoByLanguage.Where(kv => kv.Key == UILanguage.ENGLISH));
            }

            var cultureInfoToUse = matchingCultureInfos.FirstOrDefault().Value;

            System.Threading.Thread.CurrentThread.CurrentUICulture = cultureInfoToUse;
            System.Threading.Thread.CurrentThread.CurrentCulture = cultureInfoToUse;

            (Resources["LangResources"] as ObjectDataProvider).Refresh();

            // force refresh of calendar:
            var currentDisplayedDate = calendar.DisplayDate;
            calendar.DisplayDate = currentDisplayedDate.AddDays(1);
            calendar.DisplayDate = currentDisplayedDate;

            var currentDisplayedBirthday = datePickerBirthday.DisplayDate;
            datePickerBirthday.DisplayDate = currentDisplayedBirthday.AddDays(1);
            datePickerBirthday.DisplayDate = currentDisplayedBirthday;

            FillStaticContent();
        }

        private void FillStaticContent()
        {
            comboMailType.ItemsSource = Enum.GetValues<MailType>().Select(type => Mail.GetMailTypeString(type));
            if (comboMailType.SelectedIndex == -1)
            {
                comboMailType.SelectedIndex = 0;
            }

            comboPhoneType.ItemsSource = Enum.GetValues<PhoneNumberType>().Select(type => PhoneNumber.GetPhoneNumberTypeString(type));
            if (comboPhoneType.SelectedIndex == -1)
            {
                comboPhoneType.SelectedIndex = 0;
            }

            comboLanguage.ItemsSource = Enum.GetValues<UILanguage>();
            var currentLanguage = CultureInfoByLanguage.FirstOrDefault(kv => kv.Value.ThreeLetterISOLanguageName == System.Threading.Thread.CurrentThread.CurrentCulture.ThreeLetterISOLanguageName).Key;
            comboLanguage.SelectedIndex = (comboLanguage.ItemsSource as IEnumerable<UILanguage>).ToList().IndexOf(currentLanguage);

            comboBirthdayDay.ItemsSource = Enumerable.Range(1, 31).ToList();
            comboBirthdayMonth.ItemsSource = Enumerable.Range(1, 12).ToList();
            comboBirthdayYear.ItemsSource = Enumerable.Range(1900, DateTime.Now.Year - 1900 + 1).Reverse().ToList();
        }
    }

    public class LanguageResources
    {
        public Resources GetResourceInstance()
        {
            return new Resources();
        }
    }
}
