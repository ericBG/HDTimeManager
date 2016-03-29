using System;
using System.Globalization;
using System.Windows.Data;

namespace HDTimeManager
{
    /// <summary>
    /// Converts a Flags _value to a bool and back. Only supports int enums because I don't want to reflect all of the things.
    /// </summary>
    public class FlagsConverter : IValueConverter
    {
        private int _value;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            _value = (int)value;
            return (_value & (int) parameter) > 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool) value)
                return Enum.ToObject(targetType, _value | (int) parameter);
            return Enum.ToObject(targetType, _value & ~(int) parameter);
        }
    }
}