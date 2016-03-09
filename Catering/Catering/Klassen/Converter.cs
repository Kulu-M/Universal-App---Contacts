using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Catering
{

    public class NameErstellen : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var kd = value as Kunde;
            return kd.vorname + " " + kd.nachname;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public sealed class DateTimeToDateTimeOffsetConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            DateTimeOffset resultTime = (DateTime) value;
            return resultTime;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            DateTimeOffset sourceTime = (DateTimeOffset) value;
            DateTime targetTime = sourceTime.DateTime;
            return targetTime;
        }
    }
}
