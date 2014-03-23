using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cirrious.CrossCore.Converters;

namespace MyOApp.Library
{
    public class StringFormatValueConverter : MvxValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return null;

            if (parameter == null)
                return value;

            var format = "{0:" + parameter + "}";

            return string.Format(format, value);
        }
    }
}
