﻿using System;
using Cirrious.CrossCore.Converters;

namespace MyOApp.Library.Converters
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
