﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DesktopUI
{
    public class TabSizeConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var tabControl = values[0] as TabControl;
            double width = tabControl.ActualWidth / tabControl.Items.Count;
            return (width <= 1) ? 0 : (width - 1.4);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public class LanguageToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return MainWindow.LanguageNameByLanguage[value as MainWindow.UILanguage? ?? MainWindow.UILanguage.ENGLISH];
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public abstract class BoolToTextDecorationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((bool)value)
            {
                return TrueTextDecorations;
            }

            return FalseTextDecorations;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        protected TextDecorationCollection TrueTextDecorations;
        protected TextDecorationCollection FalseTextDecorations;
    }

    public class ForenameTextDecorationConverter : BoolToTextDecorationConverter
    {
        public ForenameTextDecorationConverter() : base()
        {
            TrueTextDecorations = TextDecorations.Underline;
            FalseTextDecorations = null;
        }
    }

    public class SurnameTextDecorationConverter : BoolToTextDecorationConverter
    {
        public SurnameTextDecorationConverter() : base()
        {
            TrueTextDecorations = null;
            FalseTextDecorations = TextDecorations.Underline;
        }
    }
}
