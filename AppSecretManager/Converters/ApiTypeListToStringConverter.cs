using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using AppSecretManager.Core.Models.API;

namespace AppSecretManager.Converters
{
    class ApiTypeListToStringConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var apilist = (value as List<ApiType>).Select(x => x.ToString());

            return apilist.Any() ? apilist.Aggregate((a, b) => a + ", " + b) : string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
