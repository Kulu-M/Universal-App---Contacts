using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace Catering
{
    class MyStorage
    {

        internal static async Task<bool> SchreibeObjektAsync<T>(string datei, T objekt)
        {
            try
            {
                StorageFile userdetailsfile = await ApplicationData.Current.LocalFolder.CreateFileAsync(datei, CreationCollisionOption.ReplaceExisting);
                IRandomAccessStream rndStream = await userdetailsfile.OpenAsync(FileAccessMode.ReadWrite);
                using (IOutputStream outStream = rndStream.GetOutputStreamAt(0))
                {
                    DataContractSerializer serializer = new DataContractSerializer(typeof(T));
                    serializer.WriteObject(outStream.AsStreamForWrite(), objekt);
                    var xx = await outStream.FlushAsync();
                    rndStream.Dispose();
                    outStream.Dispose();
                }
                return true;
            }
            catch (Exception x)
            {

                return false;

            }

        }

        internal static async Task<T> LeseObjektAsync<T>(string datei)
        {
            StorageFile file;
            IRandomAccessStream inStream = null;

            try
            {
                file = await
                    ApplicationData.Current.LocalFolder.GetFileAsync(datei);
                inStream = await file.OpenAsync(FileAccessMode.Read);
                DataContractSerializer serializer = new
                    DataContractSerializer(typeof(T));
                var daten = (T)serializer.ReadObject(inStream.AsStreamForRead());
                inStream.Dispose();

                return daten;
            }
            catch (Exception x)
            {
                if (inStream != null)
                    inStream.Dispose();
                return default(T);

            }
        }
    }
}
