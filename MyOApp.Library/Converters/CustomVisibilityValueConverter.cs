using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Converters;
using Cirrious.CrossCore.UI;

namespace MyOApp.Library.Converters
{
    public class CustomVisibilityValueConverter : MvxValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool visible = value != null && !string.IsNullOrEmpty(value.ToString());

            return visible ? MvxVisibility.Visible : MvxVisibility.Collapsed;
        }
        
    }
}
