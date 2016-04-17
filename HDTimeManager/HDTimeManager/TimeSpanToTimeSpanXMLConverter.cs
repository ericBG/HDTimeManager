using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace HDTimeManager
{
    public class TimeSpanToTimeSpanXMLConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is TimeSpan) && !(value is System.TimeSpan)) throw new ArgumentException("This converter only supports System.TimeSpan <-> HDTimeManager.TimeSpan");
            if (value is TimeSpan) return (System.TimeSpan) ((TimeSpan) value);
            return (TimeSpan) ((System.TimeSpan) value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert(value, targetType, parameter, culture);
        }
    }
}