using System;
using System.Globalization;
using System.Windows.Data;
using Hearthstone_Deck_Tracker.Utility.Logging;

namespace HDTimeManager
{
    public class TimespanToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is System.TimeSpan) return ((System.TimeSpan)value).ToString((parameter ?? "").ToString());
            if (value is TimeSpan) return ((System.TimeSpan)((TimeSpan)value)).ToString((parameter ?? "").ToString());
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}