using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Universal_Contacts
{
    public class Converter
    {

    }

    public class AgeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            DateTime dateofbirth = (DateTime)value;
            var now = DateTime.Now;
            var result = (now - dateofbirth).Days / 365;

            //Besser: Mit if abfragen ob now.Days kleiner als gebDate.Days ist

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
