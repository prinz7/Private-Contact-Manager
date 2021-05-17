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
        public MainWindow()
        {
            InitializeComponent();
            FillStaticContent();

            //SetCulture(new CultureInfo("en-US"));
        }

        public void SetContacts(List<Contact> contacts)
        {
            listViewContacts.ItemsSource = contacts;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(listViewContacts.ItemsSource);

            view.GroupDescriptions.Add(new PropertyGroupDescription("FirstCharacter"));
            view.SortDescriptions.Add(new SortDescription("FullName", ListSortDirection.Ascending));
        }

        public void SetCulture(CultureInfo culture)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;

            (Resources["LangResources"] as ObjectDataProvider).Refresh();

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
