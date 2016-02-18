using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Windows.Storage;
using Windows.Storage.Streams;

namespace Universal_Contacts
{
    class SaveLoad
    {
        public static async Task<bool> writeObjektAsync<T>(string file, T objekt)
        {
            try
            {
                StorageFile userdetailsfile = await
                    ApplicationData.Current.LocalFolder.CreateFileAsync(file, CreationCollisionOption.ReplaceExisting);

                IRandomAccessStream rndStream = await
                    userdetailsfile.OpenAsync(FileAccessMode.ReadWrite);

                using (IOutputStream outStream = rndStream.GetOutputStreamAt(0))
                {
                    DataContractSerializer serializer = new DataContractSerializer(typeof (T));
                    serializer.WriteObject(outStream.AsStreamForWrite(), objekt);
                    var xx = await outStream.FlushAsync();
                    rndStream.Dispose();
                    outStream.Dispose();
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }   
        }

        public static async Task<T> readObjektAsync<T>(string datei)
        {
            StorageFile file;
            IRandomAccessStream inStream = null;

            try
            {
                file = await ApplicationData.Current.LocalFolder.GetFileAsync(datei);
                inStream = await file.OpenAsync(FileAccessMode.Read);
                DataContractSerializer serializer = new DataContractSerializer(typeof(T));
                var data = (T) serializer.ReadObject(inStream.AsStreamForRead());
                inStream.Dispose();

                return data;

            }
            catch (Exception)
            {
                if (inStream != null)
                    inStream.Dispose();
                return default(T);
            }
        }

        public static async Task<T> readXmlFile<T>(string datei)
        {
            var file = await
                Windows.ApplicationModel.Package.Current.InstalledLocation.GetFileAsync(@"Data\" + datei + ".xml");
            var stream = await file.OpenReadAsync();
            var rdr = new StreamReader(stream.AsStream());
            var contents = await rdr.ReadToEndAsync();

            XmlReader reader = XmlReader.Create(new StringReader(contents));
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            var daten = (T) serializer.Deserialize(reader);
            reader.Dispose();
            return daten;
        }
    }
}
