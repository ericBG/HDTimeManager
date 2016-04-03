using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows.Data;
using Change = System.Convert;

namespace HDTimeManager
{
    public class EnumDescriptionAttributeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Enum e = value as Enum;
            if (e == null) return null;
            if (Enum.GetUnderlyingType(e.GetType()) != typeof (int)) return GetEnumDescription(e);
            return string.IsNullOrWhiteSpace(GetEnumDescription(e)) ? string.Join(", ", GetFlags(e).Select(GetEnumDescription)).Trim() : GetEnumDescription(e);
        }

        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            if (fi == null) return null;
            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        //for int enums
        private static IEnumerable<Enum> GetFlags(Enum input) => Enum.GetValues(input.GetType()).Cast<Enum>().Where(v => IsPowerOfTwo(Change.ToInt32(v)) && input.HasFlag(v));

        private static bool IsPowerOfTwo(int x) => x != 0 && (x & (x - 1)) == 0;
    }
}