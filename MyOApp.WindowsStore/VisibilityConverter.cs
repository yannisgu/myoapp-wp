using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace MyOApp.WindowsStore
{
    public class VisibilityConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string lang)
        {
            Visibility returnValue = Visibility.Collapsed;
            if (value != null)
            {
                if(value is bool)
                {
                    returnValue = (bool)value ? Visibility.Visible : Visibility.Collapsed;
                }
                else
                {
                    returnValue = !string.IsNullOrEmpty(value.ToString()) ? Visibility.Visible : Visibility.Collapsed;
                }
            }
            return returnValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string lang)
        {
            throw new NotImplementedException();
        }
    }
}
