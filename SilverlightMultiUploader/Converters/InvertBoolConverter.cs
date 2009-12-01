using System;
using System.Globalization;
using System.Windows.Data;

namespace SilverlightMultiUploader.Converters
{
    public class InvertBoolConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool actual = (bool) value;
            return !actual;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}