using DesktopUI.DataClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace DesktopUI
{
    public partial class MainWindow : Window
    {
        public enum UILanguage
        {
            ENGLISH, GERMAN
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

        public void SetContacts(List<Contact> contacts)
        {
            listViewContacts.ItemsSource = contacts;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(listViewContacts.ItemsSource);

            view.GroupDescriptions.Add(new PropertyGroupDescription("FirstCharacter"));
            view.SortDescriptions.Add(new SortDescription("FullName", ListSortDirection.Ascending));
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
