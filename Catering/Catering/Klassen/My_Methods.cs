using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Catering
{
    public class My_Methods
    {
        internal static T readSettings<T>(string key, Object initValue)
        {
            try
            {
                return (T)ApplicationData.Current.RoamingSettings.Values[key];
            }
            catch (Exception)
            {
                ApplicationData.Current.RoamingSettings.Values[key] = (T)initValue;
                return (T) initValue;
            }
        }
    }
}
