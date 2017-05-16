using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using AppSecretManager.Core.Models.API;

namespace AppSecretManager.Converters
{
    class ApiTypeToStringConverter: IValueConverter
    {
        
            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                return value?.ToString() ?? "error contact system admin";
            }

            public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                return (ApiType)Enum.Parse(typeof(ApiType), value.ToString(), true);
            }
        
    }
}
